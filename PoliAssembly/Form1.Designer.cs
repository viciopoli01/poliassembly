namespace PoliAssembly
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progress = new System.Windows.Forms.ToolStripProgressBar();
            this.tool = new System.Windows.Forms.ToolStrip();
            this.file_tool = new System.Windows.Forms.ToolStripSplitButton();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.new_project = new System.Windows.Forms.ToolStripButton();
            this.CloseTab = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.undo = new System.Windows.Forms.ToolStripButton();
            this.rendo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.run = new System.Windows.Forms.ToolStripButton();
            this.pc_increment = new System.Windows.Forms.ToolStripButton();
            this.compile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.help = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.Find = new System.Windows.Forms.ToolStripTextBox();
            this.clock = new System.Windows.Forms.Timer(this.components);
            this.info_error = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.develop_area = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stack = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.processor_registers = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.consoleTextBox1 = new PoliAssembly.consoleTextBox();
            this.statusStrip1.SuspendLayout();
            this.tool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.develop_area.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.consoleTextBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 498);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(897, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progress
            // 
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(100, 16);
            this.progress.Visible = false;
            // 
            // tool
            // 
            this.tool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file_tool,
            this.toolStripSeparator1,
            this.new_project,
            this.CloseTab,
            this.toolStripSeparator4,
            this.undo,
            this.rendo,
            this.toolStripSeparator2,
            this.run,
            this.pc_increment,
            this.compile,
            this.toolStripSeparator5,
            this.help,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.Find});
            this.tool.Location = new System.Drawing.Point(0, 0);
            this.tool.Name = "tool";
            this.tool.Size = new System.Drawing.Size(897, 25);
            this.tool.TabIndex = 3;
            this.tool.Text = "Tools";
            // 
            // file_tool
            // 
            this.file_tool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.file_tool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.exampleToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.file_tool.Image = ((System.Drawing.Image)(resources.GetObject("file_tool.Image")));
            this.file_tool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.file_tool.Name = "file_tool";
            this.file_tool.Size = new System.Drawing.Size(41, 22);
            this.file_tool.Text = "&File";
            this.file_tool.ButtonClick += new System.EventHandler(this.file_tool_ButtonClick);
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Image = global::PoliAssembly.Properties.Resources.Add_List;
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + N";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.newProjectToolStripMenuItem.Text = "New project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // exampleToolStripMenuItem
            // 
            this.exampleToolStripMenuItem.Name = "exampleToolStripMenuItem";
            this.exampleToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exampleToolStripMenuItem.Text = "Examples";
            this.exampleToolStripMenuItem.Click += new System.EventHandler(this.exampleToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Image = global::PoliAssembly.Properties.Resources.Open_Folder;
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Image = global::PoliAssembly.Properties.Resources.Save;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeyDisplayString = "Crtl + S";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Image = global::PoliAssembly.Properties.Resources.Save_as;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Image = global::PoliAssembly.Properties.Resources.Info;
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::PoliAssembly.Properties.Resources.Exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Crtl + E";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // new_project
            // 
            this.new_project.Image = global::PoliAssembly.Properties.Resources.Add_List;
            this.new_project.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.new_project.Name = "new_project";
            this.new_project.Size = new System.Drawing.Size(91, 22);
            this.new_project.Text = "&New Project";
            this.new_project.ToolTipText = "New Project (Ctrl + N)";
            this.new_project.Click += new System.EventHandler(this.new_project_Click);
            // 
            // CloseTab
            // 
            this.CloseTab.Image = global::PoliAssembly.Properties.Resources.Elimina_tab;
            this.CloseTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseTab.Name = "CloseTab";
            this.CloseTab.Size = new System.Drawing.Size(79, 22);
            this.CloseTab.Text = "Close Tab";
            this.CloseTab.ToolTipText = "Close Tab (Ctrl + X)";
            this.CloseTab.Click += new System.EventHandler(this.CloseTab_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // undo
            // 
            this.undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undo.Image = global::PoliAssembly.Properties.Resources.Undo;
            this.undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undo.Name = "undo";
            this.undo.Size = new System.Drawing.Size(23, 22);
            this.undo.Text = "Undo";
            this.undo.ToolTipText = "Undo (Ctrl + Z)";
            this.undo.Click += new System.EventHandler(this.undo_Click);
            // 
            // rendo
            // 
            this.rendo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rendo.Image = global::PoliAssembly.Properties.Resources.Redo;
            this.rendo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rendo.Name = "rendo";
            this.rendo.Size = new System.Drawing.Size(23, 22);
            this.rendo.Text = "Redo";
            this.rendo.ToolTipText = "Redo (Alt + Ctrl + Z)";
            this.rendo.Click += new System.EventHandler(this.rendo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // run
            // 
            this.run.Image = ((System.Drawing.Image)(resources.GetObject("run.Image")));
            this.run.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(48, 22);
            this.run.Text = "&Run";
            this.run.ToolTipText = "Run (F5)";
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // pc_increment
            // 
            this.pc_increment.Image = global::PoliAssembly.Properties.Resources.Plus;
            this.pc_increment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pc_increment.Name = "pc_increment";
            this.pc_increment.Size = new System.Drawing.Size(76, 22);
            this.pc_increment.Text = "Next step";
            this.pc_increment.ToolTipText = " Next (F6)";
            this.pc_increment.Click += new System.EventHandler(this.pc_increment_Click);
            // 
            // compile
            // 
            this.compile.Image = global::PoliAssembly.Properties.Resources.Collage;
            this.compile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.compile.Name = "compile";
            this.compile.Size = new System.Drawing.Size(70, 22);
            this.compile.Text = "compile";
            this.compile.Visible = false;
            this.compile.Click += new System.EventHandler(this.compile_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // help
            // 
            this.help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.help.Image = global::PoliAssembly.Properties.Resources.Help_48;
            this.help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(23, 22);
            this.help.Text = "?";
            this.help.Click += new System.EventHandler(this.help_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(37, 22);
            this.toolStripLabel1.Text = "Cerca";
            // 
            // Find
            // 
            this.Find.Name = "Find";
            this.Find.Size = new System.Drawing.Size(100, 25);
            this.Find.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Find_KeyPress);
            // 
            // clock
            // 
            this.clock.Interval = 500;
            this.clock.Tick += new System.EventHandler(this.clock_Tick);
            // 
            // info_error
            // 
            this.info_error.AutoPopDelay = 5000;
            this.info_error.InitialDelay = 500;
            this.info_error.ReshowDelay = 50;
            this.info_error.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            this.info_error.ToolTipTitle = "Error";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.develop_area);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(873, 467);
            this.splitContainer1.SplitterDistance = 430;
            this.splitContainer1.TabIndex = 11;
            // 
            // develop_area
            // 
            this.develop_area.AllowDrop = true;
            this.develop_area.Controls.Add(this.tabPage1);
            this.develop_area.Dock = System.Windows.Forms.DockStyle.Fill;
            this.develop_area.HotTrack = true;
            this.develop_area.Location = new System.Drawing.Point(0, 0);
            this.develop_area.Name = "develop_area";
            this.develop_area.SelectedIndex = 0;
            this.develop_area.Size = new System.Drawing.Size(430, 467);
            this.develop_area.TabIndex = 10;
            this.develop_area.SelectedIndexChanged += new System.EventHandler(this.develop_area_SelectedIndexChanged);
            this.develop_area.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDrop);
            this.develop_area.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnter);
            // 
            // tabPage1
            // 
            this.tabPage1.AllowDrop = true;
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(422, 441);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Welcome";
            this.tabPage1.ToolTipText = "Benvenuto";
            this.tabPage1.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDrop);
            this.tabPage1.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnter);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Courier New", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label11.Location = new System.Drawing.Point(136, 417);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(150, 12);
            this.label11.TabIndex = 5;
            this.label11.Text = "Developed by Polizzi VIncenzo";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox5.Controls.Add(this.pictureBox1);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(39, 31);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(344, 320);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PoliAssembly.Properties.Resources.Drag_And_Drop_poliass;
            this.pictureBox1.Location = new System.Drawing.Point(113, 205);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 101);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(134, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "oppure";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(79, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(186, 22);
            this.label6.TabIndex = 4;
            this.label6.Text = "File -> Examples";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label7.Location = new System.Drawing.Point(30, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(285, 22);
            this.label7.TabIndex = 3;
            this.label7.Text = "Oppure apri gli esempi in";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Location = new System.Drawing.Point(35, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(274, 22);
            this.label8.TabIndex = 2;
            this.label8.Text = "Fai click su New Project";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label9.Location = new System.Drawing.Point(101, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(142, 22);
            this.label9.TabIndex = 1;
            this.label9.Text = "Per iniziare";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label10.Location = new System.Drawing.Point(30, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(285, 22);
            this.label10.TabIndex = 0;
            this.label10.Text = "Benvenuto in PoliAssembly";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Size = new System.Drawing.Size(439, 467);
            this.splitContainer2.SplitterDistance = 328;
            this.splitContainer2.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.stack);
            this.groupBox1.Location = new System.Drawing.Point(233, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 320);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stack";
            // 
            // stack
            // 
            this.stack.AutoScroll = true;
            this.stack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stack.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.stack.Location = new System.Drawing.Point(3, 16);
            this.stack.Name = "stack";
            this.stack.Size = new System.Drawing.Size(197, 301);
            this.stack.TabIndex = 0;
            this.stack.WrapContents = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.processor_registers);
            this.groupBox2.Location = new System.Drawing.Point(3, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(224, 320);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Processor Registers";
            // 
            // processor_registers
            // 
            this.processor_registers.ColumnCount = 1;
            this.processor_registers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.processor_registers.Location = new System.Drawing.Point(6, 22);
            this.processor_registers.Name = "processor_registers";
            this.processor_registers.RowCount = 15;
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.processor_registers.Size = new System.Drawing.Size(212, 292);
            this.processor_registers.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.consoleTextBox1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(439, 135);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Console";
            // 
            // consoleTextBox1
            // 
            this.consoleTextBox1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.consoleTextBox1.AutoScrollMinSize = new System.Drawing.Size(2, 14);
            this.consoleTextBox1.BackBrush = null;
            this.consoleTextBox1.BackColor = System.Drawing.Color.DimGray;
            this.consoleTextBox1.CaretColor = System.Drawing.Color.Azure;
            this.consoleTextBox1.CharHeight = 14;
            this.consoleTextBox1.CharWidth = 8;
            this.consoleTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.consoleTextBox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.consoleTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleTextBox1.ForeColor = System.Drawing.Color.AliceBlue;
            this.consoleTextBox1.IndentBackColor = System.Drawing.Color.Gray;
            this.consoleTextBox1.IsReadLineMode = false;
            this.consoleTextBox1.IsReplaceMode = false;
            this.consoleTextBox1.LineNumberColor = System.Drawing.Color.AliceBlue;
            this.consoleTextBox1.Location = new System.Drawing.Point(3, 16);
            this.consoleTextBox1.Name = "consoleTextBox1";
            this.consoleTextBox1.Paddings = new System.Windows.Forms.Padding(0);
            this.consoleTextBox1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.consoleTextBox1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("consoleTextBox1.ServiceColors")));
            this.consoleTextBox1.ShowFoldingLines = true;
            this.consoleTextBox1.ShowLineNumbers = false;
            this.consoleTextBox1.Size = new System.Drawing.Size(433, 116);
            this.consoleTextBox1.TabIndex = 0;
            this.consoleTextBox1.Zoom = 100;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 520);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tool);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PoliAssembly";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tool.ResumeLayout(false);
            this.tool.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.develop_area.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.consoleTextBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip tool;
        private System.Windows.Forms.ToolStripButton run;
        private System.Windows.Forms.ToolStripSplitButton file_tool;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel processor_registers;
        private System.Windows.Forms.Timer clock;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripButton compile;
        private System.Windows.Forms.ToolTip info_error;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStripButton pc_increment;
        private System.Windows.Forms.ToolStripButton new_project;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton undo;
        private System.Windows.Forms.ToolStripButton rendo;
        private System.Windows.Forms.ToolStripProgressBar progress;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exampleToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton help;
        private System.Windows.Forms.TabControl develop_area;
        private System.Windows.Forms.ToolStripTextBox Find;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private consoleTextBox consoleTextBox1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.FlowLayoutPanel stack;
        private System.Windows.Forms.ToolStripButton CloseTab;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}

 