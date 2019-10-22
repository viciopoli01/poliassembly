using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Diagnostics;

namespace PoliAssembly
{
    public partial class Form1 : Form
    {
        compiler compiler = new compiler();//creo un nuovo compiler
        int line_count = 0;//mi serve per runnare il codice
        List<string> code_allocation = new List<string>();// alloco il codice dinamicamente in una lista in modo tale da avere un codice "pulito"

        FastColoredTextBox CodeSpace = new FastColoredTextBoxNS.FastColoredTextBox();
        string resource_path = @"Examples\";
        
        public Form1(string caller="")
        {
            InitializeComponent();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            
            if (caller != "")
                crea_un_nuovo_progetto(caller, false, Path.GetExtension(caller).Replace('.', ' ').Trim());
        } //inizializzo il form con il file da aprire
        

        public struct examples
        {
            public string path;
            public string name;
        }
        List<examples> examples_path = new List<examples>();

        private void Form1_Load(object sender, EventArgs e)
        {
            resource_path=AppDomain.CurrentDomain.BaseDirectory+@"Examples\";
            try
            {
                if (!Directory.Exists(resource_path))
                {
                    Directory.CreateDirectory(resource_path);
                    create_file(resource_path + "Minimo.pa", Properties.Resources.Minimo);
                    create_file(resource_path + "Fattoriale.pa", Properties.Resources.Fattoriale);
                    create_file(resource_path + "Manuale.pdf", Properties.Resources.Manuale);
                }
            }
            catch { }

            compiler.init_compiler(null);
            stack.Controls.Clear();
            init_run();
            file_tool.ShowDropDown();
            try
            {
                string[] filePaths = Directory.GetFiles(resource_path, "*.pa");
                foreach (string file in filePaths)
                {
                    string name = Path.GetFileName(file);
                    examples ex = new examples();
                    ex.name = name;
                    ex.path = file;
                    examples_path.Add(ex);
                    ToolStripMenuItem SSSMenu = new ToolStripMenuItem(name, Properties.Resources.pa_fileicon, open_examples);
                    exampleToolStripMenuItem.DropDownItems.Add(SSSMenu);
                }
            }
            catch { }

        } // form load and reset components

        private void run_Click(object sender, EventArgs e)
        {
            run_clickEvent();
        } //genera l'evento per il run
        void run_clickEvent()
        {
            if (CurrentTB != null)
            {
                if (clock.Enabled == false && compiler.is_end_buff == true && Path.GetExtension(develop_area.SelectedTab.Tag.ToString()) != ".c")
                {
                    line_count = 0; //il contatore va a 0

                    run.Image = Properties.Resources.Stop;//immagine stop del run
                    run.Text = "&Stop";

                    compiler.is_end_buff = true; //non mi aspetto nulla

                    clock.Enabled = true; //inizio ad eseguire
                }
                else
                {
                    clock.Enabled = false;// fermo tutto
                    compiler.is_end_buff = true;
                    line_count = 0;
                    run.Image = Properties.Resources.Play;
                    run.Text = "&Run";
                }
            }
        }
        /// <summary>
        /// TIMER PER RUN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clock_Tick(object sender, EventArgs e)
        {
            if (start_run() < 0)
            {
                clock.Enabled = false;
                run.Image = Properties.Resources.Play;
                run.Text = "&Run";
            }
        }

        public int start_run()
        {
            clock.Enabled = false;
            int res = run_code();
            clock.Enabled = true;
            return res;
        }

        public int run_code()
        {
            if (!compiler.is_end_buff) //se aspetto un input non faccio niente
                return 1;

            if (line_count == 0)
            {
                int error_line = prepara_codice();
                if (error_line != -1)
                {
                    MessageBox.Show("Error at line " + error_line + " " + compiler.error_string);
                    return -1;
                }

                //inizializzo il comiler e pulisco i controli
                compiler.init_compiler(code_allocation);
                stack.Controls.Clear();
                init_run();
                int count_lines = code_allocation.Count(); ;

                compiler.value_of_processor_registers[13] -= count_lines;//lo stack cresce al contrario
                compiler.code_end_address = compiler.value_of_processor_registers[13];

                consoleTextBox1.WriteLine("Program \"" + develop_area.SelectedTab.Text + "\" starts, size allocation " + count_lines + " Byte\n");

            }

            if (line_count < code_allocation.Count())
            {
                int run_out = run_line(code_allocation.ElementAt(line_count));
                if (run_out == -404)
                {
                    line_count = 0;
                    MessageBox.Show("Error!");
                    return -1;
                }
                if (run_out == -220)
                {
                    line_count = 0;
                    MessageBox.Show("Code ened!");
                    return -1;
                }
                line_count = compiler.value_of_processor_registers[12];

                if (!string.IsNullOrWhiteSpace(compiler.console_output))
                {
                    while (!compiler.is_end_buff)
                    {
                        consoleTextBox1.IsReadLineMode = true;
                        consoleTextBox1.WriteLine("Program " + develop_area.SelectedTab.Text + " says " + compiler.console_output);
                        compiler.buffer = consoleTextBox1.ReadLine();
                        compiler.func_readint();
                        consoleTextBox1.IsReadLineMode = false;
                        if (compiler.is_end_buff)
                            line_count = compiler.value_of_processor_registers[12];
                    }
                    consoleTextBox1.WriteLine("Program " + develop_area.SelectedTab.Text + " says " + compiler.console_output);

                }
                compiler.console_output = "";

                //CREO LO STACK OTTIMIZZATO 
                int record_att = compiler.code_end_address - compiler.value_of_processor_registers[13];
                if (stack.Controls.Count > record_att)
                {
                    stack.Controls.RemoveAt(stack.Controls.Count - 1);
                }
                else if (stack.Controls.Count < record_att)
                {
                    for (int address = record_att - stack.Controls.Count; address > 0; address--)
                    {
                        Panel p = compiler.add_stack(compiler.RAM[compiler.value_of_processor_registers[13] + address - 1], compiler.value_of_processor_registers[13] + address - 1);
                        p.Anchor = AnchorStyles.Left;
                        p.Anchor = AnchorStyles.Right;
                        //p.Dock = DockStyle.Top;
                        stack.Controls.Add(p);
                    }
                }

                for (int address = 0; address < stack.Controls.Count; address++)
                {
                    //Console.WriteLine("Control Address = "+address + " SP = "+ (compiler.value_of_processor_registers[13]) +" Real Address = "+ (compiler.code_end_address - address) +" RAM VALUE "+ compiler.RAM[compiler.code_end_address - address]);

                    if (stack.Controls[address].Controls[1].Text == (compiler.code_end_address - address - 1).ToString())
                    {
                        stack.Controls[address].Controls[0].Text = compiler.RAM[compiler.code_end_address - address - 1].ToString();
                    }
                }
                stack.Refresh();

                //SISTEMO I REGISTRI OTTIMIZZATO
                for (int i = 0; i < processor_registers.RowCount; i++)
                {
                    Control c = processor_registers.GetControlFromPosition(0, i);
                    //MessageBox.Show(c.Tag.ToString());
                    if (c.Tag.ToString() != compiler.value_of_processor_registers[i].ToString())
                    {
                        c.Text = compiler.registri_processore[i] + (i < 10 || i > 11 ? "  " : "") + " value -> " + (i > 12 ? compiler.value_of_processor_registers[i] : compiler.value_of_processor_registers[i]);
                        c.Tag = compiler.registri_processore[i].ToString();
                    }
                }
                processor_registers.Refresh();
            }
            else
            {
                line_count = 0;
                MessageBox.Show("Code ened!");
                return -1;
            }
            return 1;
        }

        private void compile_Click(object sender, EventArgs e)
        {
            if (develop_area.SelectedIndex > -1)
            {
                MessageBox.Show("Coming soon");
            }
        }

        public void init_run()
        {
            processor_registers.Controls.Clear();//!!!!è probabilmente meglio fare 13 label e poi fare un refresh...!!!!
            for (int i = 0; i < 15; i++)
            {
                Label reg = new Label();
                string c = (i < 10 ? "0" + i : "" + i);
                reg.Text = compiler.registri_processore[i] + (i < 10 || i > 11 ? "  " : "") + " value -> " + (i > 12 ? compiler.value_of_processor_registers[i] : compiler.value_of_processor_registers[i]);
                reg.Tag = compiler.value_of_processor_registers[i];
                processor_registers.Controls.Add(reg, 0, i);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool bHandled = false;

            // switch case is the easy way, a hash or map would be better, 
            // but more work to get set up.
            switch (keyData)
            {
                case Keys.Control | Keys.E:
                    Close();
                    break;
                case Keys.Control | Keys.N:
                    crea_un_nuovo_progetto();
                    break;
                case Keys.Control | Keys.S:
                    Save(develop_area.SelectedTab);
                    break;
                case Keys.Control | Keys.X:
                    Close_tab();
                    break;
                case Keys.Alt | Keys.Control | Keys.Z:
                    if (CurrentTB.RedoEnabled)
                        CurrentTB.Redo();
                    break;
                case Keys.F6:
                    if (CurrentTB != null && Path.GetExtension(develop_area.SelectedTab.Tag.ToString()) != ".c")
                        run_code();
                    break;
                case Keys.F5:
                    run_clickEvent();
                    break;

                case Keys.Control | Keys.Add:
                    if (CurrentTB != null)
                        CurrentTB.Zoom++;
                    break;

                case Keys.Control | Keys.Subtract:
                    if (CurrentTB != null)
                        CurrentTB.Zoom--;
                    break;

            }
            return bHandled;
        }

        //CRYPT FILE FOR SAVE EXTENSION
        static readonly string PasswordHash = "POLIMI_assembly@encrypted_password@#";
        static readonly string SaltKey = "Salte_KEYForPolImi@Assembly140230";
        static readonly string VIKey = "@This_isThe546he";

        
        private string decrypt_file_ex(string file_txt)
        {
            string new_f = "";
            try
            {
                file_txt = Decrypt(file_txt);
                var base64EncodedBytes = Convert.FromBase64String(file_txt);
                new_f = Encoding.UTF8.GetString(base64EncodedBytes);
                new_f = Decrypt(new_f);

            }
            catch (Exception)
            {
                return file_txt;
            }
            return new_f;
        }
        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        private void file_tool_ButtonClick(object sender, EventArgs e)
        {
            file_tool.ShowDropDown();
        }

        private void pc_increment_Click(object sender, EventArgs e)
        {
            if (CurrentTB != null && Path.GetExtension(develop_area.SelectedTab.Tag.ToString()) != ".c")
                run_code();
        }

        private void new_project_Click(object sender, EventArgs e)
        {
            crea_un_nuovo_progetto();
        }
        private Style sameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Gray)));
        public void crea_un_nuovo_progetto(string path = "", bool is_example = false, string type = "pa")
        {
            try
            {

                foreach (TabPage tab in develop_area.Controls)
                {
                    if ((tab.Tag != null && tab.Tag.ToString() != "") && tab.Tag.ToString() == path)
                    {
                        MessageBox.Show("Documento già aperto!");
                        return;
                    }
                }

                string title = "";
                if (string.IsNullOrWhiteSpace(path))
                {
                    title = "New_project_" + (develop_area.TabCount + 1).ToString() + ".pa";
                    //console.AppendText("Created New_project_" + (develop_area.TabCount + 1).ToString() + ".pa\n");
                    consoleTextBox1.WriteLine("Created New_project_" + (develop_area.TabCount + 1).ToString() + "." + type + "\r\n");
                }
                else
                {
                    title = Path.GetFileName(path);
                    //console.AppendText("Loaded : " + title + "\n");
                    consoleTextBox1.WriteLine("Loaded : " + title + "\r\n");
                }

                saveAsToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem.Enabled = true;

                TabPage new_proj = new TabPage();
                new_proj.Tag = is_example ? "" : path;
                new_proj.Text = title;

                var tb = new FastColoredTextBox();
                tb.Font = new Font("Curier", 10.25F);
                //tb.ContextMenuStrip = cmMain; Bello da fare!
                tb.Dock = DockStyle.Fill;
                //tb.BorderStyle = BorderStyle.Fixed3D;
                //tb.VirtualSpace = true;
                tb.LeftPadding = 17;
                tb.Language = type == "pa" ? Language.PA : Language.C;
                tb.AddStyle(sameWordsStyle);//same words style
                tb.MouseDoubleClick += removeBook;
                //tb.OpenFile(path);
                tb.Tag = new TbInfo();

                tb.ToolTipNeeded += ToolTipNeeded;

                new_proj.Controls.Add(tb);
                develop_area.TabPages.Add(new_proj);

                develop_area.SelectedTab = new_proj;

                tb.Focus();
                tb.DelayedTextChangedInterval = 1000;
                tb.DelayedEventsInterval = 500;

                tb.DragDrop += DragDrop;
                tb.DragEnter += DragEnter;

                tb.HighlightingRangeType = HighlightingRangeType.VisibleRange;

                if (path == "")
                    tb.Text = "# PoliAssembly 1.1.0v Project\r\n# Created by : " + Environment.MachineName + "\r\n# Data : " + DateTime.Now + "\r\n# info polivi@polivi.it";
                else
                    tb.Text = (type == "pa" ? decrypt_file_ex(File.ReadAllText(path)) : File.ReadAllText(path));

                tb.IsChanged = false;
                //MENU CON SUGGERIMENTI
                if (type == "pa")
                {
                    AutocompleteMenu popupMenu = new AutocompleteMenu(tb);
                    popupMenu.Opening += new EventHandler<CancelEventArgs>(popupMenu_Opening);
                    BuildAutocompleteMenu(popupMenu);
                    (tb.Tag as TbInfo).popupMenu = popupMenu;
                }

                CloseTab.Enabled = true;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                    crea_un_nuovo_progetto(path);
            }
        }

        private void removeBook(object sender, MouseEventArgs e)
        {
            if (e.X < CurrentTB.LeftIndent)
            {
                var place = CurrentTB.PointToPlace(e.Location);
                if (CurrentTB.Bookmarks.Contains(place.iLine))
                    CurrentTB.Bookmarks.Remove(place.iLine);
            }
        }

        private void ToolTipNeeded(object sender, ToolTipNeededEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.HoveredWord))
            {
                e.ToolTipTitle = "Error";
                e.ToolTipText = compiler.error_string;
            }

            /*
             * Also you can get any fragment of the text for tooltip.
             * Following example gets whole line for tooltip:
            
            var range = new Range(sender as FastColoredTextBox, e.Place, e.Place);
            string hoveredWord = range.GetFragment("[^\n]").Text;
            e.ToolTipTitle = hoveredWord;
            e.ToolTipText = "This is tooltip for '" + hoveredWord + "'";

             */
        }

        string[] snippets = {
            "LDC ^, #CONST, REGISTER\n",
            "LOAD ^, #ADDRESS, REGISTER\n",
            "STORE ^, #REGISTER, ADDRESS\n",
            "ADD ^, , # <SORG1>, <SORG2>, <DEST>\n",
            "MUL ^, , # <SORG1>, <SORG2>, <DEST>\n",
            "DIV ^, , # <SORG1>, <SORG2>, <DEST>\n",
            "MOD ^, , # <SORG1>, <SORG2>, <DEST>\n",
            "SUB ^, , # <SORG1>, <SORG2>, <DEST>\n",
            "MOD ^, , # <SORG1>, <SORG2>, <DEST>\n",
            "AND ^, , # <SORG1>, <SORG2>, <DEST>\n",
            "OR ^, , # <SORG1>, <SORG2>, <DEST>\n",
            "READINT ^ # <DEST>",
            "WRITEINT ^ # <SORG>",
            "JR ^ #LABEL\n",
            "JRN ^ #LABEL\n",
            "JRNN ^ #LABEL\n",
            "JRP ^ #LABEL\n",
            "JRNP ^ #LABEL\n",
            "JRZ ^ #LABEL\n",
            "JRNZ ^ #LABEL\n",
            "JSR ^ #LABEL_SUBRUTINE\n",
            "UNLK FP",
            "LINK FP, ^ #LABEL LINK FP, COSTANT\n",
            "RTS\n",
            "HALT\n"
        };

        private void BuildAutocompleteMenu(AutocompleteMenu popupMenu)
        {
            List<AutocompleteItem> items = new List<AutocompleteItem>();

            foreach (var item in snippets)
                items.Add(new SnippetAutocompleteItem(item));

            //set as autocomplete source
            popupMenu.Items.SetAutocompleteItems(items);
            popupMenu.SearchPattern = @"[\w\.:=!<>]";
        }

        void popupMenu_Opening(object sender, CancelEventArgs e)
        {
            //---block autocomplete menu for comments
            //get index of green style (used for comments)
            var iGreenStyle = CodeSpace.GetStyleIndex(CodeSpace.SyntaxHighlighter.GreenStyle);
            if (iGreenStyle >= 0)
                if (CodeSpace.Selection.Start.iChar > 0)
                {
                    //current char (before caret)
                    var c = CodeSpace[CodeSpace.Selection.Start.iLine][CodeSpace.Selection.Start.iChar - 1];
                    //green Style
                    var greenStyleIndex = Range.ToStyleIndex(iGreenStyle);
                    //if char contains green style then block popup menu
                    if ((c.style & greenStyleIndex) != 0)
                        e.Cancel = true;
                }
        }

        private void develop_area_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// FUNZIONI PER "COMPILARE" IL CODICE
        /// </summary>
        bool halt_find = false;
        public int prepara_codice()
        {
            code_allocation = new List<string>();
            for (int i = 0; develop_area.SelectedIndex > -1 && i < CurrentTB.Lines.Count(); i++)
            {
                string s = CurrentTB.Lines[i];
                //s = s.Replace(" ", String.Empty);
                s = s.Split('#')[0];
                //Console.WriteLine("Linea che passo :"+s);
                if (!string.IsNullOrWhiteSpace(s))
                {
                    Regex regKeywords = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    Match regMatch;
                    for (regMatch = regKeywords.Match(s); regMatch.Success; regMatch = regMatch.NextMatch())
                    {
                        int a = Convert.ToInt32(regMatch.ToString(), 16);
                        s = Regex.Replace(s, regMatch.ToString(), a.ToString());
                    }

                    if (verifica_linea(s) == 1)
                    {
                        code_allocation.Add(s);
                    }
                    else
                    {
                        CurrentTB.BookmarkColor = Color.Red;
                        CurrentTB.Bookmarks.Add(i);
                        return i + 1;
                    }
                }
            }
            if (!halt_find)
                MessageBox.Show("HALT not found! May cause problems");
            return -1;
        }

        private string[] keywords = new string[] { "LOAD", "STORE", "LDC", "ADD", "SUB", "MUL", "DIV", "OR", "AND", "MOD", "JRZ", "JRNZ", "JRN", "JRNN", "JRP", "JRNP", "JR", "PUSH", "POP", "LINK", "UNLK", "JSR", "RTS", "WRITEINT", "READINT", "HALT" };

        public int verifica_linea(string linea)
        {
            String[] split = new String[] { };
            for (int i = 0; i < keywords.Length; i++)
            {
                split = linea.Split(new string[] { keywords[i] }, StringSplitOptions.None);
                if (split.Length > 1) return error_pharser(split[1], keywords[i]);
            }
            return 0;
        }

        int error_pharser(string code_line, string func)
        {
            switch (func)
            {
                case "LOAD":
                    return compiler.compile_LOAD(code_line);
                case "STORE":
                    return compiler.compile_STORE(code_line);
                case "LDC":
                    return compiler.compile_LDC(code_line);
                case "ADD":
                    return compiler.compile_operation(code_line);
                case "SUB":
                    return compiler.compile_operation(code_line);
                case "MUL":
                    return compiler.compile_operation(code_line);
                case "DIV":
                    return compiler.compile_operation(code_line);
                case "MOD":
                    return compiler.compile_operation(code_line);
                case "OR":
                    return compiler.compile_operation(code_line);
                case "AND":
                    return compiler.compile_operation(code_line);
                case "JRZ":
                    return compiler.compile_jump(code_line);
                case "JRNZ":
                    return compiler.compile_jump(code_line);
                case "JRN":
                    return compiler.compile_jump(code_line);
                case "JRNN":
                    return compiler.compile_jump(code_line);
                case "JRP":
                    return compiler.compile_jump(code_line);
                case "JRNP":
                    return compiler.compile_jump(code_line);
                case "JR":
                    return compiler.compile_jump(code_line);
                case "JSR":
                    return compiler.compile_jump(code_line);
                case "PUSH":
                    return compiler.compile_function(code_line);
                case "POP":
                    return compiler.compile_function(code_line);
                case "LINK":
                    return compiler.compile_LINK(code_line);
                case "UNLK":
                    return compiler.compile_UNLK(code_line);
                case "WRITEINT":
                    return compiler.compile_WRITEINT(code_line);
                case "READINT":
                    return compiler.compile_READINT(code_line);
                case "HALT":
                    halt_find = true;
                    return 1;
                case "RTS":
                    return 1;
                default:
                    return 0;
            }
        }

        public int run_line(string linea)
        {
            String[] split = new String[] { };
            for (int i = 0; i < keywords.Length; i++)
            {
                split = linea.Split(new string[] { keywords[i] }, StringSplitOptions.None);
                if (split.Length > 1) return esegui_codice(split[1], keywords[i]);
            }
            return -404;//FUNCTION NOT FOUND
        }

        int esegui_codice(string code_line, string func)
        {
            switch (func)
            {
                case "LOAD":
                    return compiler.LOAD(code_line.Trim().ToUpper());
                case "STORE":
                    return compiler.STORE(code_line.Trim().ToUpper());
                case "LDC":
                    return compiler.LDC(code_line.Trim().ToUpper());
                case "ADD":
                    return compiler.ADD(code_line.Trim().ToUpper());
                case "SUB":
                    return compiler.SUB(code_line.Trim().ToUpper());
                case "MUL":
                    return compiler.MUL(code_line.Trim().ToUpper());
                case "DIV":
                    return compiler.DIV(code_line.Trim().ToUpper());
                case "MOD":
                    return compiler.MOD(code_line.Trim().ToUpper());
                case "OR":
                    return compiler.OR(code_line.Trim().ToUpper());
                case "AND":
                    return compiler.AND(code_line.Trim().ToUpper());
                case "JRZ":
                    return compiler.JRZ(code_line.Trim());
                case "JRNZ":
                    return compiler.JRNZ(code_line.Trim());
                case "JRN":
                    return compiler.JRN(code_line.Trim());
                case "JRNN":
                    return compiler.JRNN(code_line.Trim());
                case "JRP":
                    return compiler.JRP(code_line.Trim());
                case "JRNP":
                    return compiler.JRNP(code_line.Trim());
                case "JR":
                    return compiler.JR(code_line.Trim());
                case "JSR":
                    return compiler.JSR(code_line.Trim());
                case "RTS":
                    return compiler.RTS();
                case "PUSH":
                    return compiler.PUSH(code_line.Trim());
                case "POP":
                    return compiler.POP(code_line.Trim());
                case "LINK":
                    return compiler.LINK(code_line.Trim());
                case "UNLK":
                    return compiler.UNLK(code_line.Trim());
                case "WRITEINT":
                    return compiler.WRITEINT(code_line.Trim());
                case "READINT":
                    return compiler.READINT(code_line.Trim());
                case "HALT":
                    return -220;
            }
            return -404;
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crea_un_nuovo_progetto();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<TabPage> list = new List<TabPage>();
            foreach (TabPage tab in develop_area.Controls)
                list.Add(tab);
            foreach (var tab in list)
            {
                if (tab.Controls[0] is FastColoredTextBox && (tab.Controls[0] as FastColoredTextBox).IsChanged)
                {
                    switch (MessageBox.Show("Do you want save " + tab.Text + " ?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                            if (!Save(tab))
                                e.Cancel = true;
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                    }
                }
            }
        }

        private bool Save(TabPage tab)
        {
            var tb = (tab.Controls[0] as FastColoredTextBox);
            if (tab.Tag.ToString() == "")
            {
                SaveFileDialog sfdMain = new SaveFileDialog();
                // Available file extensions
                sfdMain.Filter = "pa (*.pa)|*.pa";
                // SaveFileDialog title
                sfdMain.Title = "Save";

                sfdMain.FileName = tab.Text;
                if (sfdMain.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return false;
                tab.Text = Path.GetFileName(sfdMain.FileName);
                tab.Tag = sfdMain.FileName;
            }

            try
            {
                File.WriteAllText(tab.Tag as string, tb.Text);
                tb.IsChanged = false;
                MessageBox.Show("Saved");
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    return Save(tab);
                else
                    return false;
            }

            tb.Invalidate();

            return true;
        }

        private void undo_Click(object sender, EventArgs e)
        {
            if (CurrentTB.UndoEnabled)
                CurrentTB.Undo();
        }
        private void rendo_Click(object sender, EventArgs e)
        {
            if (CurrentTB.RedoEnabled)
                CurrentTB.Redo();
        }

        /// <summary>
        /// FUNZIONI PER APRIRE E SALVARE FILE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opentext = new OpenFileDialog();
            opentext.Filter = "pa (*.pa)|*.pa | c (*.c)|*.c";
            if (opentext.ShowDialog() == DialogResult.OK)
            {
                crea_un_nuovo_progetto(opentext.FileName, false, Path.GetExtension(opentext.FileName));
                CurrentTB.IsChanged = false;
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(develop_area.SelectedTab);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(develop_area.SelectedTab);
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Info inf = new Info();
            inf.ShowDialog();
        }

        private void exampleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void open_examples(object sender, EventArgs e)
        {
            examples ex = examples_path.Find(a => a.name == sender.ToString());
            if (!String.IsNullOrWhiteSpace(ex.path))
            {
                crea_un_nuovo_progetto(ex.path);
                CurrentTB.Text = decrypt_file_ex(File.ReadAllText(ex.path));
            }
            else
            {
                MessageBox.Show("File non trovato!");
            }

        }

        private void help_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(resource_path + "Manuale.pdf");
            }
            catch { }
        }

        public void create_file(string path, byte[] file)
        {
            File.WriteAllBytes(path, file);

        }

        FastColoredTextBox CurrentTB
        {
            get
            {
                if (develop_area.SelectedTab == null)
                    return null;
                return (develop_area.SelectedTab.Controls[0] as FastColoredTextBox);
            }

            set
            {
                develop_area.SelectedTab = (value.Parent as TabPage);
                value.Focus();
            }
        }

        public class TbInfo
        {
            public AutocompleteMenu popupMenu;
        }

        bool tbFindChanged = false;
        private void Find_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && CurrentTB != null)
            {
                Range r = tbFindChanged ? CurrentTB.Range.Clone() : CurrentTB.Selection.Clone();
                tbFindChanged = false;
                r.End = new Place(CurrentTB[CurrentTB.LinesCount - 1].Count, CurrentTB.LinesCount - 1);
                var pattern = Regex.Escape(Find.Text);
                foreach (var found in r.GetRanges(pattern))
                {
                    found.Inverse();
                    CurrentTB.Selection = found;
                    CurrentTB.DoSelectionVisible();
                    return;
                }
                MessageBox.Show("Non trovato");
            }
            else
                tbFindChanged = true;
        }

        private void DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string filePath in files)
                {
                    crea_un_nuovo_progetto(filePath.ToString(), false, Path.GetExtension(filePath).Replace('.', ' ').Trim());
                }
            }
        }

        private void DragEnter(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string filePath in files)
            {
                string ex = Path.GetExtension(filePath);
                if (ex == ".pa" || ex == ".c") e.Effect = DragDropEffects.Move;
            }
        }

        /********Console********/
        bool stop;
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            consoleTextBox1.WriteLine("Welcome " + Environment.MachineName + "!\r\n");

            stop = false;
            while (stop)
            {
                if (compiler.is_end_buff == false)
                {
                    consoleTextBox1.IsReadLineMode = true;
                    consoleTextBox1.WriteLine("Program " + develop_area.SelectedTab.Text + " says " + compiler.console_output);
                    compiler.buffer = consoleTextBox1.ReadLine();
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Stop();
            base.OnClosing(e);
        }

        void Stop()
        {
            stop = true;
            consoleTextBox1.IsReadLineMode = false;
        }
        
        private void CloseTab_Click(object sender, EventArgs e)
        {
            Close_tab();
        }
        void Close_tab()
        {
            TabPage tab = develop_area.SelectedTab;
            if (develop_area.Controls.Count > 0)
            {
                if (tab.Controls[0] is FastColoredTextBox && (tab.Controls[0] as FastColoredTextBox).IsChanged)
                {
                    switch (MessageBox.Show("Do you want save " + tab.Text + " ?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case System.Windows.Forms.DialogResult.No:
                            develop_area.Controls.Remove(develop_area.SelectedTab);
                            break;
                        case System.Windows.Forms.DialogResult.Yes:
                            if (!Save(tab))
                                develop_area.Controls.Remove(develop_area.SelectedTab);
                            break;
                    }
                }
                else
                    develop_area.Controls.Remove(develop_area.SelectedTab);
            }
            if (develop_area.Controls.Count == 0)
                CloseTab.Enabled = false;
        }
    }

}
