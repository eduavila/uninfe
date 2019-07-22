using System;
using System.Windows.Forms;

namespace TesteDLL_Unimake.Business.DFe
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
            Application.Run(new FormTestarNFe());
        }
    }
}
