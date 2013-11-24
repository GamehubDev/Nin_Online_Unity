using System;
using System.Windows.Forms;

namespace Toolset
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var MainForm = new MainForm())
            {
                Application.Run(MainForm);
            }
        }
    }
}
