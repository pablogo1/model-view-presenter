using ModelViewPresenter.WindowsForms.MasterDetailWithEvents.Model;
using ModelViewPresenter.WindowsForms.MasterDetailWithEvents.View;
using ModelViewPresenter.WindowsForms.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithEvents.Presenter
{
    public class ContactsPresenter : IContactsPresenter
    {
        private readonly IContactRepository contactRepository;
        private readonly IAlertService alertService;
        private IContactsView view;

        public ContactsPresenter(IContactRepository contactRepository, IAlertService alertService)
        {
            this.contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            this.alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
        }

        public void Display()
        {
            var records = contactRepository.GetAll();
            view.Contacts = records.ToList();
        }

        public void DisplayDetail()
        {
            view.ContactId = SelectedContact?.Id;
            view.FirstName = SelectedContact?.FirstName ?? string.Empty;
            view.LastName = SelectedContact?.LastName ?? string.Empty;
            view.Phone = SelectedContact?.Phone ?? string.Empty;
        }

        public void SetView(IContactsView view)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            SetupView();
        }

        protected virtual Contact SelectedContact { get; set; }

        private void SetupView()
        {
            view.ViewLoaded += View_ViewLoaded;
            view.SelectedContactChanged += View_SelectedContactChanged;
            view.NewContactClicked += View_NewContactClicked;
            view.RemoveContactClicked += View_RemoveContactClicked;
            view.SaveContactClicked += View_SaveContactClicked;
        }

        public async Task LoadView()
        {
            await Task.Run(() => this.Display());
        }

        private void View_SaveContactClicked(object sender, ContactsViewEventArgs e)
        //public void SaveContact()
        {
            try
            {
                if (SelectedContact?.Id == null)
                {
                    SelectedContact = new Contact();
                    contactRepository.Add(SelectedContact);
                }

                SelectedContact.FirstName = view.FirstName;
                SelectedContact.LastName = view.LastName;
                SelectedContact.Phone = view.Phone;

                SelectedContact = null;
            }
            catch (Exception ex)
            {
                alertService.ShowError($"Error removing contact: {Environment.NewLine}{ex.ToString()}", "Contacts");
            }
            finally
            {
                Display();
                DisplayDetail();
            }
        }

        private void View_RemoveContactClicked(object sender, ContactsViewEventArgs e)
        //public void RemoveContact()
        {
            try
            {
                if (SelectedContact == null) return;

                contactRepository.Remove(SelectedContact);
                SelectedContact = null;
            }
            catch (Exception ex)
            {
                alertService.ShowError($"Error removing contact: {Environment.NewLine}{ex.ToString()}", "Contacts");
            }
            finally
            {
                Display();
                DisplayDetail();
            }
        }

        private void View_NewContactClicked(object sender, ContactsViewEventArgs e)
        //public void NewContact()
        {
            SelectedContact = new Contact();
            DisplayDetail();
        }

        private void View_SelectedContactChanged(object sender, ContactsViewEventArgs e)
        //public void ChangeSelectedContact(Contact selectedContact)
        {
            SelectedContact = e.SelectedContact;
            DisplayDetail();
        }

        private void View_ViewLoaded(object sender, EventArgs e)
        {
            Display();
        }
    }
}
