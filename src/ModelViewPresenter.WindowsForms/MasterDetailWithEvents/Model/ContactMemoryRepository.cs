using System.Collections.Generic;
using System.Linq;

namespace ModelViewPresenter.WindowsForms.MasterDetailWithEvents.Model
{
    public class ContactMemoryRepository : IContactRepository
    {
        private static readonly ISet<Contact> contacts = new HashSet<Contact>()
        {
            new Contact { Id = 1, FirstName = "Joe", LastName = "Doe", Phone = "(222)111-1111" }
        };

        public void Add(Contact model)
        {
            model.Id = contacts.Max(c => c.Id) + 1;
            contacts.Add(model);
        }

        public IEnumerable<Contact> GetAll()
        {
            return contacts.AsEnumerable();
        }

        public Contact GetById(int id)
        {
            return contacts.SingleOrDefault(c => c.Id == id);
        }

        public void Remove(Contact model)
        {
            contacts.Remove(model);
        }
    }
}
