using FriendOrganizer.Model;
using Kitchen.Model;
using System.Collections.Generic;

namespace Kitchen.UI.Data
{
    public abstract class Repository
    {
        public List<Customer> customers;

        public List<Meeting> meetings;

        public Repository()
        {
            customers = new List<Customer>() {
             new Customer() { Id = 1, FullName = "Thomas Abroham", Address = "jordan street 123", City="Самара" },
             new Customer() { Id = 2, FullName = "Tiara Ziemann", Address = "631 Jaden Path", City="Самара" },
             new Customer() { Id = 3, FullName = "Somanta Aysa", Address = "squard garden 795", City="Саратов" },  
             new Customer() { Id = 4, FullName = "Alfonzo Bailey", Address = "9105 Nikolaus Walk", City = "Саратов" }  };


            meetings = new List<Meeting>();
    }
    }
}
