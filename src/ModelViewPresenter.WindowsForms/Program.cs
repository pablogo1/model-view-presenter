using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

[assembly:InternalsVisibleTo("ModelViewPresenter.WindowsForms.Tests")]

namespace ModelViewPresenter.WindowsForms
{
    static class Program
    {
        private static IContainer Container { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BootstrapContainer();

            using (var scope = Container.BeginLifetimeScope())
            {
                var mainForm = Container.Resolve<MasterDetailWithDirectMethodCalls.View.IContactsView>() as Form;

                Application.Run(mainForm);
            }

        }

        private static void BootstrapContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<MainModule>();
            //builder.RegisterModule<MasterDetailWithEvents.MasterDetailWithEventsModule>();
            builder.RegisterModule<MasterDetailWithDirectMethodCalls.MasterDetailWithDirectMethodCallsModule>();

            Container = builder.Build();
        }
    }
}
