using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ModelViewPresenter.WindowsForms.MasterDetailWithDirectMethodCalls.Model;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithDirectMethodCalls.View
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

        private void ContactsForm_Load(object sender, EventArgs e)
        {
            presenter.LoadView();
        }

        private void ContactsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contact selectedContact = contactsListBox.SelectedItem as Contact;

            presenter.ChangeSelectedContact(selectedContact);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            presenter.RemoveContact();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            presenter.NewContact();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            presenter.SaveContact();
        }
    }
}
