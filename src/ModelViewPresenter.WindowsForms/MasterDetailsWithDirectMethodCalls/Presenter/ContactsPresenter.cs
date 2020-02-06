using ModelViewPresenter.WindowsForms.MasterDetailWithDirectMethodCalls.Model;
using ModelViewPresenter.WindowsForms.MasterDetailWithDirectMethodCalls.View;
using ModelViewPresenter.WindowsForms.Shared;
using System;
using System.Linq;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithDirectMethodCalls.Presenter
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

        protected virtual Contact SelectedContact { get; set; }
        
        public void LoadView()
        {
            Display();
        }

        public void ChangeSelectedContact(Contact selectedContact)
        {
            SelectedContact = selectedContact;
            DisplayDetail();
        }

        public void SaveContact()
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

        public void RemoveContact()
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

        public void NewContact()
        {
            SelectedContact = new Contact();
            DisplayDetail();
        }
    }
}
