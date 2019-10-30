using ModelViewPresenter.WindowsForms.MasterDetailWithEvents.Model;
using ModelViewPresenter.WindowsForms.MasterDetailWithEvents.View;
using ModelViewPresenter.WindowsForms.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithEvents.Presenter
{
    public interface IContactsPresenter : IMasterDetailPresenter
    {
    }

    public class ContactsPresenter : IContactsPresenter
    {
        private readonly IContactsView view;
        private readonly IContactRepository contactRepository;
        private readonly IAlertService alertService;

        public ContactsPresenter(IContactsView view, IContactRepository contactRepository, IAlertService alertService)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            this.alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));

            SetupView();
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

        protected virtual Contact SelectedContact { get; set; }

        private void SetupView()
        {
            view.ViewLoaded += View_ViewLoaded;
            view.SelectedContactChanged += View_SelectedContactChanged;
            view.NewContactClicked += View_NewContactClicked;
            view.RemoveContactClicked += View_RemoveContactClicked;
            view.SaveContactClicked += View_SaveContactClicked;
        }

        private void View_SaveContactClicked(object sender, ContactsViewEventArgs e)
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
