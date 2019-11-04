using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithEvents
{
    public class MasterDetailWithEventsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Model.ContactMemoryRepository>().As<Model.IContactRepository>();
            builder.RegisterType<Presenter.ContactsPresenter>().As<Presenter.IContactsPresenter>();
            builder.RegisterType<View.ContactsForm>()
                .As<View.IContactsView>()
                .Named<Form>("MasterDetailWithEvents")
                //.OnActivated(e =>
                //{
                //    e.Context.Resolve<Presenter.IContactsPresenter>();
                //})
                .SingleInstance();
        }
    }
}
