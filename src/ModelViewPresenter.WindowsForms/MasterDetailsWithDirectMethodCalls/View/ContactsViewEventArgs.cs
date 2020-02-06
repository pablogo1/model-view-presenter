using ModelViewPresenter.WindowsForms.MasterDetailWithDirectMethodCalls.Model;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithDirectMethodCalls.View
{
    public class ContactsViewEventArgs
    {
        public Contact SelectedContact { get; set; }

        public static ContactsViewEventArgs Empty => new ContactsViewEventArgs();
    }
}
