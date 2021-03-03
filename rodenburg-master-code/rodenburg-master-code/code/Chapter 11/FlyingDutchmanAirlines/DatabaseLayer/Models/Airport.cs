using System.Collections.Generic;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models
{
    public sealed class Airport
    {
        public int AirportId { get; set; }
        public string City { get; set; }
        public string Iata { get; set; }

        public ICollection<Flight> FlightDestinationNavigation { get; set; }
        public ICollection<Flight> FlightOriginNavigation { get; set; }

        public Airport()
        {
            FlightDestinationNavigation = new HashSet<Flight>();
            FlightOriginNavigation = new HashSet<Flight>();
        }
    }
}
