using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PoliAssembly
{
    static class Program
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string fileName = "";
            if (args != null && args.Length > 0)
                fileName = args[0];

            string[] args1 = Environment.GetCommandLineArgs();
            SingleInstanceController control = new SingleInstanceController();

            control.set_file_path(fileName);
            control.Run(args1);
        }
    }
    public class SingleInstanceController : WindowsFormsApplicationBase {
        string file_path = "";
        public SingleInstanceController()
        {
            IsSingleInstance = true;

            StartupNextInstance += this_StartupNextInstance;
        }

        void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            Form1 form = MainForm as Form1; //My derived form type
            form.crea_un_nuovo_progetto(e.CommandLine[1],false,Path.GetExtension(e.CommandLine[1]).Replace('.',' ').Trim());
        }

        protected override void OnCreateMainForm()
        {
            MainForm = new Form1(file_path);
        }
        public void set_file_path(string args)
        {
            file_path = args;
        }
    }
}
