using ModelViewPresenter.WindowsForms.MasterDetailWithEvents.Model;
using ModelViewPresenter.WindowsForms.MasterDetailWithEvents.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithEvents.View
{
    public interface IContactsView
    {
        IList<Contact> Contacts { set; }
        int? ContactId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }

        event EventHandler ViewLoaded;
        event EventHandler<ContactsViewEventArgs> SelectedContactChanged;
        event EventHandler<ContactsViewEventArgs> SaveContactClicked;
        event EventHandler<ContactsViewEventArgs> RemoveContactClicked;
        event EventHandler<ContactsViewEventArgs> NewContactClicked;
    }
}
