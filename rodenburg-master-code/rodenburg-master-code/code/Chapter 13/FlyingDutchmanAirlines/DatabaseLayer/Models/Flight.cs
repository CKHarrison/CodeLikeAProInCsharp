using System.Collections.Generic;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models
{
    public sealed class Flight
    {
        public int FlightNumber { get; set; }
        public int Origin { get; set; }
        public int Destination { get; set; }

        public Airport DestinationNavigation { get; set; }
        public Airport OriginNavigation { get; set; }
        public ICollection<Booking> Booking { get; set; }

        public Flight()
        {
            Booking = new HashSet<Booking>();
        }
    }
}
