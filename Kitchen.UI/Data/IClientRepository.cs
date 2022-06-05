using FriendOrganizer.Model;
using Kitchen.Model;
using System.Collections.Generic;

namespace Kitchen.UI.Data
{
    public interface IClientRepository
    {
        Customer GetById(int id);
        void Add(Customer customer);
        IEnumerable<Customer> GetAll();

        bool HasChanges();

        bool HasMeetings(int id);

        void RemovePhoneNumber(FriendPhoneNumber model);

        void Remove(Customer customer);
         
        void Save();
    }
}