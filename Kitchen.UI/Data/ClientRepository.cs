using FriendOrganizer.Model;
using Kitchen.Model;
using System.Collections.Generic;
using System.Linq;

namespace Kitchen.UI.Data
{
    public class ClientRepository : Repository, IClientRepository
    {

        public void Add(Customer customer) => customers.Add(customer);

        public Customer GetById(int id) => customers.Single(f => f.Id == id);

        public IEnumerable<Customer> GetAll() => customers;

        public bool HasChanges() => false;


        public bool HasMeetings(int id)
        {
            //return await Context.Meetings.AsNoTracking()
            //  .Include(m => m.Friends)
            //  .AnyAsync(m => m.Friends.Any(f => f.Id == id));

            return false;
        }

        public void RemovePhoneNumber(FriendPhoneNumber model)
        {
            //Context.FriendPhoneNumbers.Remove(model);
        }

        public void Remove(Customer customer)
        {
            customers.Remove(customer);
        }

        public void Save()
        {
            /* to do */
        }
    }
}
