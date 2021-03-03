using FlyingDutchmanAirlines.ControllerLayer.JsonData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlyingDutchmanAirlines_Tests.ControllerLayer.JsonData
{
    [TestClass]
    public class BookingDataTests
    {
        [TestMethod]
        public void BookingData_ValidData()
        {
            BookingData bookingData = new BookingData {FirstName = "Marina", LastName = "Michaels"};
            Assert.AreEqual("Marina", bookingData.FirstName);
            Assert.AreEqual("Michaels", bookingData.LastName);
        }

        [TestMethod]
        [DataRow("Mike", null)]
        [DataRow(null, "Morin")]
        public void BookingData_InvalidData_NullPointers(string firstName, string lastName)
        {
            BookingData bookingData = new BookingData { FirstName = firstName, LastName = lastName };
            Assert.AreEqual(firstName, bookingData.FirstName);
            Assert.AreEqual(lastName, bookingData.LastName);
        }

        [TestMethod]
        [DataRow("Eleonor", "")]
        [DataRow("", "Wilke")]
        public void BookingData_InvalidData_EmptyStrings(string firstName, string lastName)
        {
            BookingData bookingData = new BookingData { FirstName = firstName, LastName = lastName };
            Assert.AreEqual(firstName, bookingData.FirstName ?? "");
            Assert.AreEqual(lastName, bookingData.LastName ?? "");
        }
    }
}
