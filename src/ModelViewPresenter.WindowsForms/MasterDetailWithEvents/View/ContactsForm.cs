using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ModelViewPresenter.WindowsForms.MasterDetailWithEvents.Model;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithEvents.View
{
    public partial class ContactsForm : Form, IContactsView
    {
        public ContactsForm()
        {
            InitializeComponent();
        }

        public ContactsForm(Presenter.IContactsPresenter presenter) : this()
        {
            this.presenter = presenter;
            this.presenter.SetView(this);
        }

        public IList<Contact> Contacts
        {
            set
            {
                contactsListBox.DisplayMember = "FullName";
                contactsListBox.ValueMember = "Id";
                contactsListBox.DataSource = value;
            }
        }

        private int? selectedContactId;
        private readonly Presenter.IContactsPresenter presenter;

        public int? ContactId
        {
            get => selectedContactId;
            set
            {
                selectedContactId = value;
                idLabel.Text = $"Id: {selectedContactId?.ToString() ?? string.Empty}";
            }
        }

        public string FirstName { get => firstNameTextBox.Text; set => firstNameTextBox.Text = value; }
        public string LastName { get => lastNameTextBox.Text; set => lastNameTextBox.Text = value; }
        public string Phone { get => phoneTextBox.Text; set => phoneTextBox.Text = value; }

        public event EventHandler ViewLoaded;
        public event EventHandler<ContactsViewEventArgs> SelectedContactChanged;
        public event EventHandler<ContactsViewEventArgs> SaveContactClicked;
        public event EventHandler<ContactsViewEventArgs> RemoveContactClicked;
        public event EventHandler<ContactsViewEventArgs> NewContactClicked;

        protected virtual void OnViewLoaded()
        {
            ViewLoaded?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSelectedContactChanged()
        {
            var handler = SelectedContactChanged;

            if (handler != null)
            {
                Contact selectedContact = contactsListBox.SelectedItem as Contact;
                var eventArgs = new ContactsViewEventArgs
                {
                    SelectedContact = selectedContact
                };

                handler(this, eventArgs);
            }
        }

        protected virtual void OnSaveContactClicked()
        {
            var eventArgs = ContactsViewEventArgs.Empty;
            SaveContactClicked?.Invoke(this, eventArgs);
        }

        protected virtual void OnRemoveContactClicked()
        {
            var eventArgs = ContactsViewEventArgs.Empty;
            RemoveContactClicked?.Invoke(this, eventArgs);
        }

        protected virtual void OnNewContactClicked()
        {
            var eventArgs = ContactsViewEventArgs.Empty;
            NewContactClicked?.Invoke(this, eventArgs);
        }

        private void ContactsForm_Load(object sender, EventArgs e)
        {
            OnViewLoaded();
        }

        private void ContactsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedContactChanged();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            OnRemoveContactClicked();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            OnNewContactClicked();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            OnSaveContactClicked();
        }
    }
}
