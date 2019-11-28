using ModelViewPresenter.WindowsForms.MasterDetailWithEvents.Model;
using ModelViewPresenter.WindowsForms.MasterDetailWithEvents.View;
using ModelViewPresenter.WindowsForms.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithEvents.Presenter
{
    public class ContactsPresenter : MasterDetailPresenter<IContactsView>, IContactsPresenter
    {
        private readonly IContactRepository contactRepository;
        private readonly IAlertService alertService;

        public ContactsPresenter(IContactRepository contactRepository, IAlertService alertService)
        {
            this.contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            this.alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
        }

        protected override void Display()
        {
            var records = contactRepository.GetAll();
            View.Contacts = records.ToList();
        }

        protected override void DisplayDetail()
        {
            View.ContactId = SelectedContact?.Id;
            View.FirstName = SelectedContact?.FirstName ?? string.Empty;
            View.LastName = SelectedContact?.LastName ?? string.Empty;
            View.Phone = SelectedContact?.Phone ?? string.Empty;
        }

        protected override void SetupView()
        {
            View.ViewLoaded += View_ViewLoaded;
            View.SelectedContactChanged += View_SelectedContactChanged;
            View.NewContactClicked += View_NewContactClicked;
            View.RemoveContactClicked += View_RemoveContactClicked;
            View.SaveContactClicked += View_SaveContactClicked;
        }
        
        protected virtual Contact SelectedContact { get; set; }

        private void View_SaveContactClicked(object sender, ContactsViewEventArgs e)
        {
            try
            {
                if (SelectedContact?.Id == null)
                {
                    SelectedContact = new Contact();
                    contactRepository.Add(SelectedContact);
                }

                SelectedContact.FirstName = View.FirstName;
                SelectedContact.LastName = View.LastName;
                SelectedContact.Phone = View.Phone;

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
        {
            SelectedContact = new Contact();
            DisplayDetail();
        }

        private void View_SelectedContactChanged(object sender, ContactsViewEventArgs e)
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
