using Kitchen.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FriendOrganizer.Model
{
    public class Meeting
    {
        public Meeting() => Customers = new Collection<Customer>();

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
