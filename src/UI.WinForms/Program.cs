using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exceptionless;
using Infrastructure.Modules;
using Ninject;

namespace UI.WinForms
{
    static class Program
    {
        private static StandardKernel kernel;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Bootstrap();
            Application.Run(kernel.Get<MainForm>());
        }

        static void Bootstrap()
        {
            ExceptionlessClient.Default.Register();
            kernel = new StandardKernel(new NetOfficeModule());
            kernel.Bind<MainForm>().ToSelf();
        }
    }
}
