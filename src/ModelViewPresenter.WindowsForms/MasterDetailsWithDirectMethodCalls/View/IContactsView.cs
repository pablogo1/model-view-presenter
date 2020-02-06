using ModelViewPresenter.WindowsForms.MasterDetailWithDirectMethodCalls.Model;
using System.Collections.Generic;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithDirectMethodCalls.View
{
    public interface IContactsView
    {
        IList<Contact> Contacts { set; }
        int? ContactId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }
    }
}
