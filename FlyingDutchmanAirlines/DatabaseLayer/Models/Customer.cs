using System;
using System.Collections.Generic;

#nullable disable

namespace FlyingDutchmanAirlines.DatabaseLayer.Models
{
    public sealed class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public ICollection<Booking> Bookings
        {
            get; set;
        }
        public Customer(string name)
        {
            Bookings = new HashSet<Booking>();
            Name = name;
        }

       
    }
}
