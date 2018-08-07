using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HelpMe.BusinessAccess;

namespace HelpMeSync
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] strArgs = Environment.GetCommandLineArgs();
            if (strArgs.Length > 1 && strArgs[1].Trim().ToUpper() == "HelpMeSync".ToUpper())
            {
                try { Application.Run(new frmMain()); }
                catch { Application.Exit(); }
            }
            else
            {
                Application.Run(new frmMDI());
            }
        }
    }
}