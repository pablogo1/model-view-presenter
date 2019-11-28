using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelViewPresenter.WindowsForms.MasterDetailWithEvents.Model;
using ModelViewPresenter.WindowsForms.MasterDetailWithEvents.Presenter;
using ModelViewPresenter.WindowsForms.MasterDetailWithEvents.View;
using ModelViewPresenter.WindowsForms.Shared;
using Moq;

namespace ModelViewPresenter.WindowsForms.Tests.MasterDetailWithEventsTests
{
    public class TestContactsPresenter : ContactsPresenter
    {
        public TestContactsPresenter(IContactRepository contactRepository, IAlertService alertService)
            : base(contactRepository, alertService)
        {
        }

        public new Contact SelectedContact
        {
            get => base.SelectedContact;
            set => base.SelectedContact = value;
        }

        public new void Display()
        {
            base.Display();
        }

        public new void DisplayDetail()
        {
            base.DisplayDetail();
        }
    }

    public class ContactsPresenterTests
    {
        [TestClass]
        public class ContactsPresenter_DisplayTests
        {
            private TestContactsPresenter presenter;
            private Mock<IContactRepository> repositoryMock;
            private Mock<IAlertService> alertServiceMock;
            private Mock<IContactsView> viewMock;

            [TestInitialize]
            public void TestInitialize()
            {
                repositoryMock = new Mock<IContactRepository>();
                alertServiceMock = new Mock<IAlertService>();
                viewMock = new Mock<IContactsView>();

                presenter = new TestContactsPresenter(repositoryMock.Object, alertServiceMock.Object);
                presenter.SetView(viewMock.Object);

                viewMock.SetupSet(p => p.Contacts = It.IsAny<IList<Contact>>())
                    .Verifiable();
                repositoryMock.Setup(m => m.GetAll())
                    .Returns(() => new List<Contact>()
                    {
                    new Contact { Id = 1, FirstName = "Joe", LastName = "Doe" }
                    })
                    .Verifiable();
            }

            [TestMethod]
            public void Display_GetsAllContactsFromRepository_AndSetsViewContactsProperty()
            {
                presenter.Display();

                viewMock.Verify();
                repositoryMock.Verify();
            }

        }

        [TestClass]
        public class ContactsPresenter_DisplayDetailTests
        {
            private TestContactsPresenter presenter;
            private Mock<IContactRepository> repositoryMock;
            private Mock<IAlertService> alertServiceMock;
            private Mock<IContactsView> viewMock;

            [TestInitialize]
            public void TestInitialize()
            {
                repositoryMock = new Mock<IContactRepository>();
                alertServiceMock = new Mock<IAlertService>();
                viewMock = new Mock<IContactsView>();

                presenter = new TestContactsPresenter(repositoryMock.Object, alertServiceMock.Object);
                presenter.SetView(viewMock.Object);
            }

            [TestMethod]
            public void DisplayDetail_ShouldSetViewProperties_WithSelectedContactValues()
            {
                var contact = new Contact
                {
                    Id = 1,
                    FirstName = "Joe",
                    LastName = "Doe",
                    Phone = "(111)231-1232"
                };

                viewMock.SetupSet(p => p.ContactId = It.Is<int>(v => v == contact.Id))
                    .Verifiable();
                viewMock.SetupSet(p => p.FirstName = It.Is<string>(v => v == contact.FirstName))
                    .Verifiable();
                viewMock.SetupSet(p => p.LastName = It.Is<string>(v => v == contact.LastName))
                    .Verifiable();
                viewMock.SetupSet(p => p.Phone = It.Is<string>(v => v == contact.Phone))
                    .Verifiable();

                presenter.SelectedContact = contact;

                presenter.DisplayDetail();

                viewMock.Verify();
            }

            [TestMethod]
            public void DisplayDetail_ShouldSetNullIdAndEmptyStringsOnViewProperties_WhenSelectedContactIsNull()
            {
                viewMock.SetupSet(p => p.ContactId = It.Is<int?>(v => !v.HasValue))
                    .Verifiable();
                viewMock.SetupSet(p => p.FirstName = It.Is<string>(v => v == string.Empty))
                    .Verifiable();
                viewMock.SetupSet(p => p.LastName = It.Is<string>(v => v == string.Empty))
                    .Verifiable();
                viewMock.SetupSet(p => p.Phone = It.Is<string>(v => v == string.Empty))
                    .Verifiable();

                presenter.SelectedContact = null;

                presenter.DisplayDetail();

                 viewMock.Verify();
            }
        }

        [TestClass]
        public class ContractsPresenter_EventsRaisedByViewTests
        {
            private ContactsPresenter presenter;
            private Mock<IContactRepository> repositoryMock;
            private Mock<IAlertService> alertServiceMock;
            private Mock<IContactsView> viewMock;

            [TestInitialize]
            public void TestInitialize()
            {
                repositoryMock = new Mock<IContactRepository>();
                alertServiceMock = new Mock<IAlertService>();
                viewMock = new Mock<IContactsView>();

                presenter = new ContactsPresenter(repositoryMock.Object, alertServiceMock.Object);
                presenter.SetView(viewMock.Object);

                viewMock.SetupSet(p => p.Contacts = It.IsAny<IList<Contact>>())
                    .Verifiable();
                repositoryMock.Setup(m => m.GetAll())
                    .Returns(() => new List<Contact>()
                    {
                    new Contact { Id = 1, FirstName = "Joe", LastName = "Doe" }
                    })
                    .Verifiable();
            }

            [TestMethod]
            public void Presenter_ShouldDisplay_WhenViewRaisesViewLoadedEvent()
            {
                viewMock.Raise(p => p.ViewLoaded += null, EventArgs.Empty);

                viewMock.Verify();
                repositoryMock.Verify();
            }
        }
        
    }
}
