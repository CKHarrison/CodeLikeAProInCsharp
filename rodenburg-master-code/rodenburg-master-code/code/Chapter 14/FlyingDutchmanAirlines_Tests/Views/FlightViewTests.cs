using FlyingDutchmanAirlines.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlyingDutchmanAirlines_Tests.Views
{
    [TestClass]
    public class FlightViewTests
    {
        [TestMethod]
        public void Constructor_FlightView_Success()
        { 
            string flightNumber = "0";
            string originCity = "Amsterdam";
            string originCityCode = "AMS";
            string destinationCity = "Moscow";
            string destinationCityCode = "SVO";

            FlightView view = new FlightView(flightNumber, (originCity, originCityCode), (destinationCity, destinationCityCode));
            Assert.IsNotNull(view);

            Assert.AreEqual(view.FlightNumber, flightNumber);
            Assert.AreEqual(view.Origin.City, originCity);
            Assert.AreEqual(view.Origin.Code, originCityCode);
            Assert.AreEqual(view.Destination.City, destinationCity);
            Assert.AreEqual(view.Destination.Code, destinationCityCode);
        }

        [TestMethod]
        public void Constructor_FlightView_Success_FlightNumber_Null()
        {
            string originCity = "Athens";
            string originCityCode = "ATH";
            string destinationCity = "Dubai";
            string destinationCityCode = "DXB";

            FlightView view = new FlightView(null, (originCity, originCityCode), (destinationCity, destinationCityCode));
            Assert.IsNotNull(view);

            Assert.AreEqual(view.FlightNumber, "No flight number found");
            Assert.AreEqual(view.Origin.City, originCity);
            Assert.AreEqual(view.Destination.City, destinationCity);
        }

        [TestMethod]
        public void Constructor_AirportStruct_Success_City_EmptyString()
        {
            string destinationCity = string.Empty;
            string destinationCityCode = "SYD";

            AirportStruct airportStruct = new AirportStruct((destinationCity, destinationCityCode));
            Assert.IsNotNull(airportStruct);

            Assert.AreEqual(airportStruct.City, "No city found");
            Assert.AreEqual(airportStruct.Code, destinationCityCode);
        }

        [TestMethod]
        public void Constructor_AirportStruct_Success_Code_EmptyString()
        {
            string destinationCity = "Ushuaia";
            string destinationCityCode = string.Empty;

            AirportStruct airportStruct = new AirportStruct((destinationCity, destinationCityCode));
            Assert.IsNotNull(airportStruct);

            Assert.AreEqual(airportStruct.City, destinationCity);
            Assert.AreEqual(airportStruct.Code, "No code found");
        }
    }
}
