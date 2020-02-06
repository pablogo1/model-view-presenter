using Autofac;
using System.Windows.Forms;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithDirectMethodCalls
{
    public class MasterDetailWithDirectMethodCallsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Model.ContactMemoryRepository>().As<Model.IContactRepository>();
            builder.RegisterType<Presenter.ContactsPresenter>().As<Presenter.IContactsPresenter>();
            builder.RegisterType<View.ContactsForm>()
                .As<View.IContactsView>()
                .Named<Form>("MasterDetailWithDirectMethodCalls");
        }
    }
}
