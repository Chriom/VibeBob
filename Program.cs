using System;
using System.Windows.Forms;

namespace SuperMegaJeuDuPatrickPong
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // OH! JE SUIS PR T! JE SUIS PR T! JE SUIS PR     T!
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormulaireDuKrabsKrusty());
        }
    }
}