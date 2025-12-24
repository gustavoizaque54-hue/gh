using System;
using System.Windows.Forms;

namespace Gx_cheat
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ClientApp()); // FORM DE LOGIN
        }
    }
}