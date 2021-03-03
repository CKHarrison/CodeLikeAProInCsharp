using System.Collections.Generic;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models
{
    public partial class Flight
    {
        public Flight()
        {
            Booking = new HashSet<Booking>();
        }

        public int FlightNumber { get; set; }
        public int Origin { get; set; }
        public int Destination { get; set; }

        public Airport DestinationNavigation { get; set; }
        public Airport OriginNavigation { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
    }
}
