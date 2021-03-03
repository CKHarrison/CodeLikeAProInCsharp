namespace FlyingDutchmanAirlinesExisting.Objects
{
    public class Flight
    {
        public int FlightNumber;
        public int OriginID;
        public int DestinationID;

        public Flight(int flightNumber, int originID, int destinationID)
        {
            FlightNumber = flightNumber;
            OriginID = originID;
            DestinationID = destinationID;
        }
    }
}


