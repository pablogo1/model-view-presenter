using Autofac;

namespace ModelViewPresenter.WindowsForms
{
    public class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Shared.MessageBoxAlertService>().As<Shared.IAlertService>();
        }
    }
}
