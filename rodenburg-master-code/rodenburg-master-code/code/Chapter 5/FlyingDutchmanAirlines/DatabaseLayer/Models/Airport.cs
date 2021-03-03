using System.Collections.Generic;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models
{
    public partial class Airport
    {
        public Airport()
        {
            FlightDestinationNavigation = new HashSet<Flight>();
            FlightOriginNavigation = new HashSet<Flight>();
        }

        public int AirportId { get; set; }
        public string City { get; set; }
        public string Iata { get; set; }

        public virtual ICollection<Flight> FlightDestinationNavigation { get; set; }
        public virtual ICollection<Flight> FlightOriginNavigation { get; set; }
    }
}
