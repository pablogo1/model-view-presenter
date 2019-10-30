using ModelViewPresenter.WindowsForms.MasterDetailWithEvents.Model;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithEvents.View
{
    public class ContactsViewEventArgs
    {
        public Contact SelectedContact { get; set; }

        public static ContactsViewEventArgs Empty => new ContactsViewEventArgs();
    }
}
