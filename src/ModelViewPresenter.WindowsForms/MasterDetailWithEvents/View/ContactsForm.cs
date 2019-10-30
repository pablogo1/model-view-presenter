using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
