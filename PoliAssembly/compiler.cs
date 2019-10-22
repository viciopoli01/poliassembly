using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PoliAssembly
{
    public class compiler
    {
        public int[] RAM = new int[2048];//1Kb

        public string[] registri_processore = new string[15];
        public int[] value_of_processor_registers = new int[15];
        private int last_operation_reult = 1;
        public string error_string="";
        public int grandezza_parole = 4;
        public string console_output = "";
        public string buffer =""; //buffer per writeint
        public bool is_end_buff = true;
        public int code_end_address = 0;//indica il minimo pc (massimo) per creare lo stack di controlli serve nel form1
        public struct funzioni
        {
            public int add;
            public string str;
        }
        List<funzioni> func = new List<funzioni>();

        List<string> code_space = new List<string>();

        public compiler()
        {
            for (int i = 0; i < 12; i++)
                registri_processore[i]="R" + i;
            registri_processore[12] = "PC";
            registri_processore[13] = "SP";
            registri_processore[14] = "FP";
            
        }

        public void init_compiler(List<string> code) {
            code_space = code;
            func.Clear();
            Array.Clear(RAM, 0, RAM.Count());
            Array.Clear(value_of_processor_registers, 0, value_of_processor_registers.Count());
            value_of_processor_registers[13] = 523;
            value_of_processor_registers[14] = 523;
            is_end_buff = true;
        }

        string[] errors = {
            "CONST, REGISTER",   //0 
            "ADDRESS, REGISTER",   //1
            "REGISTER, ADDRESS",     //2
            "<SORG1>, <SORG2>, <DEST>",   //3
            "<DEST>",   //4
            "<SORG>",   //5
            "LABEL",    //6
            "LABEL LINK FP, COSTANT",    //7
            "ADDRESS MUST BE LESS THAN 2048"    //8
        };
        /// <summary>
        /// LOAD AND STORE FUNCTION 
        /// </summary>
        /// <param name="func_string"></param>
        /// <returns></returns>
        public int LOAD(string func_string)
        {
            int[] ind=analyze_string_load(func_string);
            if(ind[1]>=0 && ind[0]>=0)
            {
                if (ind[0] > 2047)
                {
                    error_string = "Error  " + errors[8];
                    return -1;
                }
                value_of_processor_registers[ind[1]] = RAM[ind[0]];
                //Console.WriteLine("///// LOAD "+func_string);
                //Console.WriteLine(registri_processore[ind[1]]+" -> "+ind[0]+"("+ RAM[ind[0]] + ")");
                //Console.WriteLine("/////");
                value_of_processor_registers[12]++;
                return 1;
            }
            return 0;
        }
        public int compile_LOAD(string func_string)
        {
            int[] ind = analyze_string_load(func_string,true);
            //Console.WriteLine("Risultato load "+ func_string + " compile : " + ind[0] + ", " + ind[1]);
            if (ind[0] >= 0 && ind[1] >= 0)
                return 1;
            error_string = "Syntaxt error Expected : " + errors[1];
            return 0;
        }

        public int[] analyze_string_load(string an,bool is_compile=false)
        {
            an=an.Replace(" ", String.Empty);
            an = an.Split('#')[0];

            string[] params_ = an.Split(',');
            int[] index = new int[2] { -1, -1 };

            if (params_.Count() != 2)
                return index;

            ////////////////Verifico se il primo parametro è un indirizzo in memoria
            string[] reg = params_[0].Split('(', ')');
            if (reg.Count() > 1 && !String.IsNullOrEmpty(reg[1]) && registri_processore.Contains(reg[1].Trim()))
            {
                //QUESTA PARTE è CORRETTA////////////////
                int value = 0;
                reg[1] = reg[1].Trim();
                index[0] = (!is_compile ? value_of_processor_registers[Array.IndexOf(registri_processore, reg[1])] : 1);
                reg = params_[0].Split('+', '(');
                if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                    value = (!is_compile ? (Int32.Parse(reg[1]) / grandezza_parole) : 1);
                reg = params_[0].Split('-', '(');
                if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                    value = (!is_compile ? -1*(Int32.Parse(reg[1]) / grandezza_parole) : 1);
                index[0] = (!is_compile ? index[0] + value : 1);
                //////////////////////////////////////////
            }
            else
            {
                int res = 0;
                if (int.TryParse(params_[0], out res))
                    index[0] = (!is_compile ? (res > 0 ? res : -1) : 1);
            }
            if (registri_processore.Contains(params_[1].Trim()))//se non è il secondo parametro deve ritornarmi il valore del registro e non lindice che mi serve solo per allocare
                index[1] = (!is_compile ? Array.IndexOf(registri_processore, params_[1].Trim()) : 1);
            
            return index;
        }

        public int STORE(string func_string)
        {
            int[] ind = analyze_string_store(func_string);
            //if (compile_STORE(func_string)!=0)
            //{
            if (ind[1] >= 0)
            {
                if (ind[1] > 2047)
                {
                    error_string = "Error  " + errors[8];
                    return -1;
                }
                RAM[ind[1]] = ind[0];
                //Console.WriteLine("///// STORE " + func_string);
                //Console.WriteLine("///// VALORE DI R0 " + value_of_processor_registers[0]);
                //Console.WriteLine(ind[1] + " -> " + ind[0]);
                //Console.WriteLine("/////");
                value_of_processor_registers[12]++;
                return 1;
            }
            //}
            return 0;
        }
        public int compile_STORE(string func_string)
        {
            error_string = "";
            int[] ind = analyze_string_store(func_string,true);
            //Console.WriteLine("Risultato store " + func_string + " compile : " + ind[0] + ", " + ind[1]);
            if (ind[0] >= 0 && ind[1] >= 0)
                return 1;
            error_string = "Syntaxt error Expected : " + errors[2];
            return 0;
        }

        public int[] analyze_string_store(string an, bool is_compile=false)
        {
            an = an.Replace(" ", String.Empty);
            an = an.Split('#')[0];

            string[] params_ = an.Split(',');
            int[] index = new int[2]{ -1,-1 };
            if (params_.Count() != 2)
                return index;
            
            ////////////////Verifico se il primo parametro è un registro
            if (registri_processore.Contains(params_[0].Trim()))//se non è il secondo parametro deve ritornarmi il valore del registro e non lindice che mi serve solo per allocare
                index[0] = (!is_compile ? value_of_processor_registers[Array.IndexOf(registri_processore, params_[0].Trim())] : 1);
            ////////////////Verifico se il secondo parametro è un indirizzo in memoria
            string[] reg = params_[1].Split('(', ')');
            if (reg.Count() > 1 && !String.IsNullOrEmpty(reg[1]) && registri_processore.Contains(reg[1].Trim()))
            {
                //QUESTA PARTE è CORRETTA////////////////
                int value = 0;
                reg[1] = reg[1].Trim();
                index[1] = (!is_compile ? value_of_processor_registers[Array.IndexOf(registri_processore, reg[1])] : 1);
                reg = params_[1].Split('+', '(');
                if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                    value = (!is_compile ? (Int32.Parse(reg[1]) / grandezza_parole) : 1);
                reg = params_[1].Split('-', '(');
                if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                    value = (!is_compile ?  -(Int32.Parse(reg[1])/ grandezza_parole) : 1);
                index[1] = (!is_compile ? index[1] + value : 1);
                //////////////////////////////////////////
            }
            else
            {
                int res = 0;
                if (int.TryParse(params_[1], out res))
                    index[1] = (!is_compile ? (res > 0 ? res : -1) : 1);
            }

            return index;
        }

        /// <summary>
        /// LOAD CONSTANT FUNCTION
        /// </summary>
        /// <param name="func_string"></param>
        /// <returns></returns>
        public int LDC(string func_string)
        {
            int[] ind = analyze_string_ldc(func_string);
            //if (compile_LDC(func_string)!=0)
            //{
                value_of_processor_registers[ind[1]] = ind[0];

                //Console.WriteLine("///// LDC " + func_string);
                //Console.WriteLine(registri_processore[ind[1]] + " -> " + ind[0]);
                //Console.WriteLine("/////");

                value_of_processor_registers[12]++;
                return 1;
            //}
            //return 0;
        }
        public int compile_LDC(string func_string)
        {
            error_string = "";
            int[] ind = analyze_string_ldc(func_string,true);
            if (ind[0] >= 0 && ind[1] >= 0)
                return 1;
            error_string = "syntaxt LDC CONST,REG";
            return 0;
        }

        public int[] analyze_string_ldc(string an,bool is_compile=false)
        {
            an = an.Replace(" ", String.Empty);
            an = an.Split('#')[0];

            string[] params_ = an.Split(',');
            int[] index = new int[2] { -1, -1 };

            if (params_.Count() != 2)
                return index;

            ////verifico che il primo fattore sia una costante
            int res = 0;
            string[] reg = params_[0].Split('$');
            if (reg.Count() > 1 && !String.IsNullOrEmpty(reg[1]))
            {
                if (int.TryParse(reg[1].Trim(), out res))
                    index[0] = (!is_compile ? res : 1);
            }
            ////verifico che il secondo fattore sia una registro
            if (registri_processore.Contains(params_[1].Trim()))
                    index[1] = Array.IndexOf(registri_processore, params_[1]);
                        
            return index;
        }

        /// <summary>
        /// OPERATION FUNCTION
        /// </summary>
        /// <param name="an"></param>
        /// <returns></returns>
        public int ADD(string func_string) {
            int[] ind = analyze_string_operation(func_string);
            if (compile_operation(func_string) != 0)
            {
                
                value_of_processor_registers[ind[2]] = ind[0]+ ind[1];
                last_operation_reult = value_of_processor_registers[ind[2]];

                //if (ind[3] != 0 )//diminuisco lo stack se sottraggo un numero parole di memoria a sp
                  //  stack.Pop();
                //Console.WriteLine("///// SUM " + func_string);
                //Console.WriteLine( registri_processore[ind[2]]+" -> "+ ind[0]+ " + "+  ind[1] +"="+ last_operation_reult);
                //Console.WriteLine("/////");

                value_of_processor_registers[12]++;
                return 1;
            }
            error_string = "syntaxt SUM PROC_REG,PROC_REG,PROC_REG";
            return 0;
        }

        public int SUB(string func_string)
        {
            int[] ind = analyze_string_operation(func_string);
            if (compile_operation(func_string) != 0)
            {
                value_of_processor_registers[ind[2]] = ind[0] - ind[1];
                last_operation_reult = value_of_processor_registers[ind[2]];
                
                //if (ind[3] != 0)//aumento lo stack se aggiunbgo parole di memoria a sp
                   // stack.Push(add_stack(RAM[value_of_processor_registers[13]]));
                //Console.WriteLine("///// SUB " + func_string);
                //Console.WriteLine(registri_processore[ind[2]] + " -> " + ind[0] + " - " + ind[1] + "=" + last_operation_reult);
                //Console.WriteLine("/////");

                value_of_processor_registers[12]++;
                return 1;
            }
            error_string = "syntaxt SUB PROC_REG,PROC_REG,PROC_REG";
            return 0;
        }

        public int MUL(string func_string)
        {
            int[] ind = analyze_string_operation(func_string);
            if (compile_operation(func_string) != 0)
            {
                value_of_processor_registers[ind[2]] = ind[0] * ind[1];
                last_operation_reult = value_of_processor_registers[ind[2]];
                Console.WriteLine("///// MUL " + func_string);
                Console.WriteLine(registri_processore[ind[2]] + " -> " + ind[0] + " * " + ind[1] + "=" + last_operation_reult);
                Console.WriteLine("/////");
                value_of_processor_registers[12]++;
                return 1;
            }
            return 0;
        }

        public int MOD(string func_string)
        {
            int[] ind = analyze_string_operation(func_string);
            if (compile_operation(func_string) != 0)
            {
                value_of_processor_registers[ind[2]] = ind[0] % ind[1];
                last_operation_reult = value_of_processor_registers[ind[2]];
                //Console.WriteLine("///// MUL " + func_string);
                //Console.WriteLine(registri_processore[ind[2]] + " -> " + ind[0] + " * " + ind[1] + "=" + last_operation_reult);
                //Console.WriteLine("/////");
                value_of_processor_registers[12]++;
                return 1;
            }
            return 0;
        }

        public int DIV(string func_string)
        {
            int[] ind = analyze_string_operation(func_string);
            if (compile_operation(func_string) != 0)
            {
                value_of_processor_registers[ind[2]] = ind[0] / ind[1];
                last_operation_reult = value_of_processor_registers[ind[2]];
                //Console.WriteLine("///// DIV " + func_string);
                //Console.WriteLine(registri_processore[ind[2]] + " -> " + ind[0] + " / " + ind[1] + "=" + last_operation_reult);
                //Console.WriteLine("/////");
                value_of_processor_registers[12]++;
                return 1;
            }
            return 0;
        }

        public int OR(string func_string)
        {
            int[] ind = analyze_string_operation(func_string);
            if (compile_operation(func_string) != 0)
            {
                value_of_processor_registers[ind[2]] = ind[0] | ind[1];
                last_operation_reult = value_of_processor_registers[ind[2]];
                //Console.WriteLine("///// OR " + func_string);
                //Console.WriteLine(registri_processore[ind[2]] + " -> " + ind[0] + " | " + ind[1] + "=" + last_operation_reult);
                //Console.WriteLine("/////");
                value_of_processor_registers[12]++;
                return 1;
            }
            return 0;
        }

        public int AND(string func_string)
        {
            int[] ind = analyze_string_operation(func_string);
            //Console.WriteLine("Risultato ldc compile : " + ind[0] + ", " + ind[1] + ", " + ind[2]);
            if (compile_operation(func_string)!=0)
            {
                value_of_processor_registers[ind[2]] = ind[0] & ind[1];
                last_operation_reult = value_of_processor_registers[ind[2]];
                //Console.WriteLine("///// AND " + func_string);
                //Console.WriteLine(registri_processore[ind[2]] + " -> " + ind[0] + " & " + ind[1] + "=" + last_operation_reult);
                //Console.WriteLine("/////");
                value_of_processor_registers[12]++;
                return 1;
            }
            return 0;
        }

        public int compile_operation(string func_string)
        {
            error_string = "";
            int[] ind = analyze_string_operation(func_string,true);
            if (ind[0] >= 0 && ind[1] >= 0 && ind[2] >= 0)
                return 1;
            error_string = "Syntaxt error Expected : " + errors[3];
            return 0;
        }
        
        public int[] analyze_string_operation(string an,bool is_compile=false)
        {
            an = an.Replace(" ", String.Empty);
            an = an.Split('#')[0];

            string[] params_ = an.Split(',');
            int[] index = new int[4] { -1, -1, -1, 0 };// il quarto parametro lo considero per vedere se è un incremento di sp pc ecc
            int is_on_reg = 1;
            
            if (params_.Count() !=3)
            {
                return index;
            }
            if (registri_processore.Contains(params_[2].Trim()))
            {
                index[2] = Array.IndexOf(registri_processore, params_[2]);
                if (index[2] > 11)
                    index[3] = 1;
            }

            for (int p = 0; p < 2; p++)
            {
                //Console.WriteLine("Stringa splittata da virgola : " + params_[p]);
                string[] reg = params_[p].Split('(', ')');
                if (reg.Count() > 1 && !String.IsNullOrEmpty(reg[1]))
                {
                    //trova +-N(reg) 
                    int value = 0;
                    reg[1] = reg[1].Trim();
                    index[p] = (!is_compile?value_of_processor_registers[Array.IndexOf(registri_processore, reg[1])]:1);
                    reg = params_[p].Split('+', '(');
                    if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                        value = Int32.Parse(reg[1]);

                    reg = params_[p].Split('-', '(');
                    if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                        value = (!is_compile? -1 * Int32.Parse(reg[1]):1);

                    index[p] = RAM[index[p] + value/grandezza_parole];
                }
                else
                {
                    //trova $const, int, RN
                    int res = 0;
                    reg = params_[p].Split('$');
                    if (reg.Count() > 1 && !String.IsNullOrEmpty(reg[1]))
                    {
                        if (int.TryParse(reg[1].Trim(), out res))
                            index[p] = (!is_compile ? res/is_on_reg : 1);
                    }
                    else if (registri_processore.Contains(params_[p].Trim()))
                    {
                        index[p] = (!is_compile ? value_of_processor_registers[Array.IndexOf(registri_processore, params_[p])] : 1);
                        
                        if (p == 1 && Array.IndexOf(registri_processore, params_[p])>11)
                            index[0] /= 4;
                        if (p == 0 && Array.IndexOf(registri_processore, params_[p]) > 11)
                            is_on_reg = 4;
                    }
                    
                }
            }
            return index;
        }

        /// <summary>
        /// JUMP FUNCTION
        /// </summary>
        /// <param name="func_string"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public int JRZ(string func_string)
        {
            string res = analyze_string_jump(func_string);
            if (!String.IsNullOrWhiteSpace(res) && last_operation_reult==0)
            {
                value_of_processor_registers[12] = find_jump(res);
                return 1;
            }
            value_of_processor_registers[12]++;
            return 0;
        }

        public int JRNZ(string func_string)
        {
            string res = analyze_string_jump(func_string);
            if (!String.IsNullOrWhiteSpace(res) && last_operation_reult!=0)
            {
                value_of_processor_registers[12] = find_jump(res);
                return 1;
            }
            value_of_processor_registers[12]++;
            return 0;
        }

        public int JRN(string func_string)
        {
            string res = analyze_string_jump(func_string);
            if (!String.IsNullOrWhiteSpace(res) && last_operation_reult<0)
            {
                value_of_processor_registers[12] = find_jump(res);
                return 1;
            }
            value_of_processor_registers[12]++;
            return 0;
        }

        public int JRNN(string func_string)
        {
            string res = analyze_string_jump(func_string);
            if (!String.IsNullOrWhiteSpace(res) && last_operation_reult>=0)
            {
                value_of_processor_registers[12] = find_jump(res);
                return 1;
            }
            value_of_processor_registers[12]++;
            return 0;
        }

        public int JRP(string func_string)
        {
            string res = analyze_string_jump(func_string);
            if (!String.IsNullOrWhiteSpace(res) && last_operation_reult>0)
            {
                value_of_processor_registers[12] = find_jump(res);
                return 1;
            }
            value_of_processor_registers[12]++;
            return 0;
        }

        public int JRNP(string func_string)
        {
            string res = analyze_string_jump(func_string);
            if (!String.IsNullOrWhiteSpace(res) && last_operation_reult<0)
            {
                value_of_processor_registers[12] = find_jump(res);
                return 1;
            }
            value_of_processor_registers[12]++;
            return 0;
        }

        public int JR(string func_string)
        {
            string res = analyze_string_jump(func_string);
            if (!String.IsNullOrWhiteSpace(res))
            {
                value_of_processor_registers[12] = find_jump(res);
                return 1;
            }
            return 0;
        }

        public int JSR(string func_string)
        {
            string res = analyze_string_jump(func_string);
            if (!String.IsNullOrWhiteSpace(res))
            {
                value_of_processor_registers[13]--;
                RAM[value_of_processor_registers[13]] = value_of_processor_registers[12];//salvo il pc
                //Console.WriteLine("into " + find_jump(res, rc));
                value_of_processor_registers[12] = find_jump(res);//salto nel label
                              //stack.Push(add_stack(RAM[value_of_processor_registers[13]],func_string));

                funzioni f = new funzioni();
                f.add = value_of_processor_registers[13];
                f.str = func_string;
                func.Add(f);
                return 1;
            }
            return 0;
        }

        public int compile_jump(string func_string)
        {
            error_string = "";
            string res = analyze_string_jump(func_string);
            if (!String.IsNullOrWhiteSpace(res))
            {
                return 1;
            }
            error_string = "Syntaxt error Expected : " + errors[6];
            return 0;
        }

        public int find_jump(string jump)
        {
            for(int i = 0;i<code_space.Count();i++)
            {
                string[] find = code_space[i].Split(new string[] { jump }, StringSplitOptions.None);
                if (find.Count() > 1) {
                    string[] split = find[0].Split(new string[] { "JSR" }, StringSplitOptions.None);
                    if (i != value_of_processor_registers[12] && split.Count() < 2)
                    {
                        return i;
                    }
                }
            }
            /*int new_pc = code_space.GetLineFromCharIndex(code_space.Find(jump));
            i = code_space.Find(jump);
            if (new_pc == value_of_processor_registers[12])
                new_pc = code_space.GetLineFromCharIndex(code_space.Find(jump, i + 1, RichTextBoxFinds.MatchCase));

            string vre_jsr = code_space.Lines[new_pc];
            string[] split = vre_jsr.Split(new string[] { "JSR" }, StringSplitOptions.None);
            if(split.Count() > 1)
                new_pc = code_space.GetLineFromCharIndex(code_space.Find(jump, i + 1, RichTextBoxFinds.MatchCase));*/
            //Console.WriteLine("new pc ... "+ new_pc);
            return 0;
        }

        public string analyze_string_jump(string jump)
        {
            jump = jump.Replace(" ", String.Empty);
            jump = jump.Split('#')[0];
            jump = jump.Trim();
            //Console.WriteLine("SALTA AL LABEL "+jump);
            if (jump.Length > 0)
            {
                return jump;
            }
            return null;
        }

        /// <summary>
        /// FUNCTION
        /// </summary>
        /// <param name="func_string"></param>
        /// <returns></returns>       
        
        public int PUSH(string func_string)
        {
            int[] ind = analyze_string_push(func_string);
            //Console.WriteLine("PUSH VALUE "+ind[0]);
            if (ind[0] >= 0) {
                value_of_processor_registers[12]++;//aumento pc
                value_of_processor_registers[13]--;
                RAM[value_of_processor_registers[13]] = value_of_processor_registers[ind[0]];
                //stack.Push(add_stack(RAM[value_of_processor_registers[13]]));
                return 1;
            }
            return 0;
        }

        public int POP(string func_string)
        {
            int[] ind = analyze_string_push(func_string);
            if (ind[0] >= 0)
            {
                
                //Console.WriteLine("dentro POP R"+ind[0]+" value " + RAM[value_of_processor_registers[13]]);
                value_of_processor_registers[ind[0]] = RAM[value_of_processor_registers[13]];
                value_of_processor_registers[12]++;//incremento sp
                value_of_processor_registers[13]++;


                //stack.Pop();
                //add_row_stack(stack, new String[] { "" + value_of_processor_registers[13], "" + RAM[value_of_processor_registers[13]] });
                return 1;
            }
            return 0;
        }

        public int compile_function(string an)
        {
            int[] ind = analyze_string_push(an,true);
            if (ind[0] >= 0)
                return 1;
            error_string = "Syntaxt error Expected : REGISTER";
            return 0;
        }

        public int[] analyze_string_push(string an,bool is_compile=false)
        {
            an = an.Split('#')[0];
            int[] index = new int[1] { -1};

            if (String.IsNullOrWhiteSpace(an))
                return index;
            if (registri_processore.Contains(an.Trim()))//se non è il primo parametro deve ritornarmi il valore del registro e non lindice che mi serve solo per allocare
                index[0] = (!is_compile ? Array.IndexOf(registri_processore, an.Trim()) : 1);

            return index;
        }

        public int LINK(string func_string)
        {            
            if (compile_LINK(func_string)!=0)
            {
                int[] ind = analyze_string_link(func_string);
                
                //RAM[value_of_processor_registers[13]] = ind[0];
                ind[1] /= grandezza_parole;
                value_of_processor_registers[13]--;
                RAM[value_of_processor_registers[13]] = value_of_processor_registers[14];
                //stack.Push(add_stack(RAM[value_of_processor_registers[13]],"FP precedente"));
                
                funzioni f = new funzioni();
                f.add = value_of_processor_registers[13];
                f.str = "FP precedente";
                func.Add(f);

                value_of_processor_registers[14] = value_of_processor_registers[13];//di solito setto fp allo stack corrente

                while (ind[1]>0)
                {
                    value_of_processor_registers[13]--;
                    RAM[value_of_processor_registers[13]] = 0;
                    funzioni f1 = new funzioni();
                    f1.add = value_of_processor_registers[13];
                    f1.str = "Local var";
                    func.Add(f1);
                    //stack.Push(add_stack(RAM[value_of_processor_registers[13]],"Local var"));
                    ind[1]--;
                }
                value_of_processor_registers[13] = value_of_processor_registers[13];//setto sp a max
                value_of_processor_registers[12]++;//incremento pc
                return 1;
            }
            return 0;
        }
        public int compile_LINK(string func_string)
        {
            int[] ind = analyze_string_link(func_string,true);
            if (ind[0]>=0 && ind[1]>=0)
                return 1;
            error_string = "Syntaxt error Expected : " + errors[7];
            return 0;
        }

        public int[] analyze_string_link(string an, bool is_compile = false)
        {
            //an = an.Replace(" ", String.Empty);
            //an = an.Split('#')[0];
            string[] params_ = an.Split(',');
            int[] index = new int[2] { -1, -1 };
            if (params_.Count() > 2)
                return index;
            for (int p = 0; p < params_.Count(); p++)
            {
                //Console.WriteLine("Stringa splittata da virgola : " + params_[p]);
                string[] reg = params_[p].Split('(', ')');
                if (reg.Count() > 1 && !String.IsNullOrEmpty(reg[1]) && registri_processore.Contains(reg[1].Trim()))
                {
                    //trova +-N(reg) 
                    int value = 0;
                    reg[1] = reg[1].Trim();
                    index[p] = (!is_compile ? RAM[value_of_processor_registers[Array.IndexOf(registri_processore, reg[1])]] : 1);
                    reg = params_[p].Split('+', '(');
                    if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                        value = Int32.Parse(reg[1]);
                    reg = params_[p].Split('-', '(');
                    if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                        value = (!is_compile ? -1 * Int32.Parse(reg[1]) : 1);
                    index[p] = index[p] + value / grandezza_parole;
                }
                else
                {
                    //trova $const, int, RN
                    int res = 0;
                    reg = params_[p].Split('$');
                    if (reg.Count() > 1 && !String.IsNullOrEmpty(reg[1]))
                    {
                        if (int.TryParse(reg[1].Trim(), out res))
                            index[p] = (!is_compile ? res : 1);
                    }
                    else if (int.TryParse(params_[p], out res))
                        index[p] = res;
                    else if (registri_processore.Contains(params_[p].Trim()))
                        index[p] = (!is_compile ? Array.IndexOf(registri_processore, params_[p]) : 1);
                }
            }

            return index;
        }
        
        public int UNLK(string func_string)
        {
            if (compile_UNLK(func_string) != 0)
            {
                int[] ind = analyze_string_unlink(func_string);
                int diff_sp_fp = -value_of_processor_registers[13] + value_of_processor_registers[14];

                value_of_processor_registers[13] = value_of_processor_registers[14]+1;//sp = fp
                /*for (int i = 0; i <= diff_sp_fp; i++)
                {
                    stack.Pop();
                }*/
                value_of_processor_registers[14] = RAM[value_of_processor_registers[14]];//fp
                value_of_processor_registers[12]++;//incremento pc
                //RAM[value_of_processor_registers[13]] = ind[0];
                return 1;
            }
            return 0;
        }
        public int compile_UNLK(string func_string)
        {
            int[] ind = analyze_string_unlink(func_string,true);
            if (ind[0] >=0)
                return 1;
            error_string = "Syntaxt error Expected : FP";
            return 0;
        }

        public int[] analyze_string_unlink(string an, bool is_compile = false)
        {
            int[] index = new int[1] { -1 };
            string[] reg = an.Split('(', ')');
            if (reg.Count() > 1 && !String.IsNullOrEmpty(reg[1]) && registri_processore.Contains(reg[1].Trim()))
            {
                //trova +-N(reg) 
                int value = 0;
                reg[1] = reg[1].Trim();
                index[0] = (!is_compile ? RAM[value_of_processor_registers[Array.IndexOf(registri_processore, reg[1])]] : 1);
                reg = an.Split('+', '(');
                if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                    value = Int32.Parse(reg[1]);
                reg = an.Split('-', '(');
                if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                value = (!is_compile ? -1 * Int32.Parse(reg[1]) : 1);
                index[0] = index[0] + value;
            }
            else
            {
                 if (registri_processore.Contains(an.Trim()))
                    index[0] = (!is_compile?Array.IndexOf(registri_processore, an) :1);
            }

            return index;
        }

        public int RTS()
        {
            if (RAM[value_of_processor_registers[13]] != 0)
            {
                value_of_processor_registers[12] = RAM[value_of_processor_registers[13]] + 1;
                value_of_processor_registers[13]++;
                //stack.Pop();
                return 1;
            }
            return 0;
        }

        public int WRITEINT(string func_string) {
            int[] ind = analyze_string_writeint(func_string);
            //Console.WriteLine("CONSOLE OUTPUT : " + ind[0]);
            if (compile_WRITEINT(func_string)!=0)
            {
                console_output = console_output + "" + ind[0];
                //Console.WriteLine("CONSOLE OUTPUT : " + ind[0]);
                value_of_processor_registers[12]++;
                return 1;
            }
            return 0;
        }

        public int compile_WRITEINT(string func_string)
        {
            if (analyze_string_writeint(func_string, true)[0] >= 0)
                return 1;
            return 0;
        }

        public int[] analyze_string_writeint(string an, bool is_compile = false)
        {
            an = an.Replace(" ", String.Empty);
            an = an.Split('#')[0];
            error_string = "Syntaxt error";
            int[] index = new int[1] { -1 };
            string[] reg = an.Split('(', ')');
            if (reg.Count() > 1 && !String.IsNullOrEmpty(reg[1]) && registri_processore.Contains(reg[1].Trim()))
            {
                //PARTE CORRETTA
                int value = 0;
                index[0] = (!is_compile ? value_of_processor_registers[Array.IndexOf(registri_processore, reg[1])] : 1);

                reg = an.Split('+', '(');
                if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                    value = Int32.Parse(reg[1]);
                reg = an.Split('-', '(');
                if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                    value = (!is_compile ? -1 * Int32.Parse(reg[1]) : 1);
                index[0] = index[0] + value / grandezza_parole;
            }
            else
            {
                //PARTE CORRETTA
                int res = 0;
                if (int.TryParse(an, out res))
                {
                    if(res<2047)
                        index[0] = (!is_compile ? RAM[res] : 1);//se non è il secondo parametro deve ritornarmi per forza l'indece del registro
                    else
                        error_string = "Error  " + errors[8];
                }
                else if (registri_processore.Contains(an.Trim()))//se non è il primo parametro deve ritornarmi il valore del registro e non lindice che mi serve solo per allocare
                    index[0] = (!is_compile ? value_of_processor_registers[Array.IndexOf(registri_processore, an)] : 1);
                
            }
            return index;
        }

        int address_readint_allocation = -1;
        public int READINT(string func_string)
        {
            int[] ind = analyze_string_readint(func_string);
            if (compile_READINT(func_string) != 0)
            {
                buffer = "";
                address_readint_allocation = ind[0];
                console_output = "Write a number :";
                is_end_buff = false;
                return 1;
            }
            return 0;
        }

        public void func_readint()
        {
            if(address_readint_allocation<0)
            {
                console_output = "Error no address to store value;";
                return;
            }
            int value = 0;
            if(Int32.TryParse(buffer,out value))
            {
                console_output = "Ok!";
                is_end_buff = true;
            }
            else
            {
                console_output = "Write a number :";
                is_end_buff = false;
                return;
            }
            RAM[address_readint_allocation] = value;
            
            Console.WriteLine("func ecc " + address_readint_allocation +"->"+ RAM[address_readint_allocation]);
            address_readint_allocation = -1;
            value_of_processor_registers[12]++;
        }

        public int compile_READINT(string func_string)
        {
            if (analyze_string_readint(func_string, true)[0] >= 0)
                return 1;
            if (analyze_string_readint(func_string, true)[0] == -404)
                error_string = "Error  " + errors[8];
            return 0;
        }

        public int[] analyze_string_readint(string an, bool is_compile = false)
        {
            an = an.Replace(" ", String.Empty);
            int[] index = new int[1] { -1 };
            string[] reg = an.Split('(', ')');
            if (reg.Count() > 1 && !String.IsNullOrEmpty(reg[1]) && registri_processore.Contains(reg[1].Trim()))
            {
                //PARTE CORRETTA
                int value = 0;
                index[0] = (!is_compile ? value_of_processor_registers[Array.IndexOf(registri_processore, reg[1])] : 1);

                reg = an.Split('+', '(');
                if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                    value = Int32.Parse(reg[1]);
                reg = an.Split('-', '(');
                if (reg.Count() > 2 && !String.IsNullOrEmpty(reg[1]))
                    value = (!is_compile ? -1 * Int32.Parse(reg[1]) : 1);
                index[0] = index[0] + value / grandezza_parole;
            }
            else
            {
                //PARTE CORRETTA
                int res = -1;
                if (int.TryParse(an, out res))
                    index[0] = (!is_compile ? (res > 0 ? res : -1) : 1);//se non è il secondo parametro deve ritornarmi per forza l'indece del registro

            }
            if (index[0] > 2047)
                return new int[1]{ -404};
            return index;
        }

        /// <summary>
        /// halt function
        /// </summary>
        /// <returns></returns>
        public int HALT()
        {
            return value_of_processor_registers[12]=-1;//end value for program is -40
        }

        /// <summary>
        /// MODIFY STACK
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="rowElements"></param>
        /*public void add_row_stack(TableLayoutPanel panel, string[] rowElements)
        {
            if (panel.ColumnCount != rowElements.Length)
                throw new Exception("Elements number doesn't match!");
            //get a reference to the previous existent row
            RowStyle temp = panel.RowStyles[panel.RowCount - 1];
            //increase panel rows count by one
            panel.RowCount++;
            //add a new RowStyle as a copy of the previous one
            panel.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
            //add the control
            for (int i = 0; i < rowElements.Length; i++)
            {
                panel.Controls.Add(new Label() { Text = rowElements[i] }, i, panel.RowCount - 1);
            }
        }*/
        public Panel add_stack(int data,int row_add)
        {
            /****cella****/
            Panel sta = new Panel();
            sta.BackColor = Color.LightGray;
            sta.BorderStyle = BorderStyle.FixedSingle;
            sta.Dock = DockStyle.Fill;
            sta.Height = 30;
            /****valore****/
            Label value = new Label();
            value.Text = ""+data;
            value.Font = new Font("Curier", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); ;
            sta.Controls.Add(value);
            value.Dock = DockStyle.Right;
            /****ADDRESS****/
            Label addr = new Label();
            addr.Text = "" + row_add;
            addr.Font = new Font("Curier", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); ;
            sta.Controls.Add(addr);
            addr.Dock = DockStyle.Left;

            if (func.FindIndex(a=>a.add== row_add)>-1)
            {
                funzioni f = func.Find(a => a.add == row_add);
                Label label = new Label();//NOME FUNZIONE

                label.Text = f.str;
                label.Font = new Font("Curier", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                sta.Controls.Add(label);
                label.Dock = DockStyle.Left;
            }
            
            return sta;
        }
    }
}
