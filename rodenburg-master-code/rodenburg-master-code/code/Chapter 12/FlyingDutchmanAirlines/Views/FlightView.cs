namespace FlyingDutchmanAirlines.Views
{
    public class FlightView
    {
        public string FlightNumber { get; private set; }
        public AirportStruct Origin { get; private set; }
        public AirportStruct Destination { get; private set; }
        public FlightView(string flightNumber, (string city, string code) origin, (string city, string code) destination)
        {
            FlightNumber = string.IsNullOrEmpty(flightNumber) ? "No flight number found" : flightNumber;

            Origin = new AirportStruct(origin);
            Destination = new AirportStruct(destination);
        }
    }

    public struct AirportStruct
    {
        public string City { get; private set; }
        public string Code { get; private set; }

        public AirportStruct((string city, string code) airport)
        {
            City = string.IsNullOrEmpty(airport.city) ? "No city found" : airport.city;
            Code = string.IsNullOrEmpty(airport.code) ? "No code found" : airport.code;
        }
    }
}
