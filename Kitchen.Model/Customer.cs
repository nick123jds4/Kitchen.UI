using FriendOrganizer.Model;
using System;
using System.Collections.Generic;

namespace Kitchen.Model
{
    public class Customer
    {
        public Customer()
        {
            PhoneNumbers = new List<FriendPhoneNumber>();
            Meetings = new List<Meeting>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }

        public string City { get; set; }
        public DateTime? DateOfMeasurement { get; set; }
        public ICollection<FriendPhoneNumber> PhoneNumbers { get; set; }

        public ICollection<Meeting> Meetings { get; set; }
    }
}
