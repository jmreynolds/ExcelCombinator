using System;
using System.Windows.Forms;
using Exceptionless;
using Infrastructure.Modules;
using Ninject;

namespace UI.WinForms
{
    static class Program
    {
        private static StandardKernel _kernel;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Bootstrap();
            Application.Run(_kernel.Get<MainForm>());
        }

        static void Bootstrap()
        {
            ExceptionlessClient.Default.Register();
            _kernel = new StandardKernel(new NetOfficeModule());
            _kernel.Bind<MainForm>().ToSelf();
        }
    }
}
