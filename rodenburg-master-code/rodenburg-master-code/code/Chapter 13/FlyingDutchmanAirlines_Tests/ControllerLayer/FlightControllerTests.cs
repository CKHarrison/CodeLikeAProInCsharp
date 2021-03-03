using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.ControllerLayer;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.ServiceLayer;
using FlyingDutchmanAirlines.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FlyingDutchmanAirlines_Tests.ControllerLayer
{
    [TestClass]
    public class FlightControllerTests
    {
        [TestMethod]
        public async Task GetFlights_Success()
        {
            Mock<FlightService> service = new Mock<FlightService>();

            List<FlightView> returnFlightViews = new List<FlightView>(2)
            {
                new FlightView("1932",("Groningen", "GRQ"), ("Phoenix", "PHX")),
                new FlightView("841", ("New York City", "JFK"), ("London", "LHR"))
            };

            service.Setup(s => s.GetFlights()).Returns(FlightViewAsyncGenerator(returnFlightViews));

            FlightController controller = new FlightController(service.Object);
            ObjectResult response = await controller.GetFlights() as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

            Queue<FlightView> content = response.Value as Queue<FlightView>;
            Assert.IsNotNull(content);

            Assert.IsTrue(returnFlightViews.All(flight => content.Contains(flight)));
        }

        private async IAsyncEnumerable<FlightView> FlightViewAsyncGenerator(IEnumerable<FlightView> views)
        { 
            foreach (FlightView flightView in views) 
            { 
                yield return flightView; 
            } 
        }

        [TestMethod]
        public async Task GetFlights_Failure_FlightNotFoundException_404()
        {
            Mock<FlightService> service = new Mock<FlightService>();
            service.Setup(s => s.GetFlights()).Throws(new FlightNotFoundException());

            FlightController controller = new FlightController(service.Object);
            ObjectResult response = await controller.GetFlights() as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("No flights were found in the database", response.Value);
        }

        [TestMethod]
        public async Task GetFlights_Failure_ArgumentException_500()
        {
            Mock<FlightService> service = new Mock<FlightService>();
            service.Setup(s => s.GetFlights()).Throws(new ArgumentException());

            FlightController controller = new FlightController(service.Object);
            ObjectResult response = await controller.GetFlights() as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.AreEqual("An error occurred", response.Value);
        }


        [TestMethod]
        public async Task GetFlightByFlightNumber_Success()
        {
            Mock<FlightService> service = new Mock<FlightService>();

            FlightView returnedFlightView = new FlightView("0", ("Lagos", "LOS"), ("Marrakesh", "RAK"));
            service.Setup(s => s.GetFlightByFlightNumber(0)).Returns(Task.FromResult(returnedFlightView));

            FlightController controller = new FlightController(service.Object);

            ObjectResult response = await controller.GetFlightByFlightNumber(0) as ObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

            FlightView content = response.Value as FlightView;
            Assert.IsNotNull(content);

            Assert.AreEqual(returnedFlightView, content);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(1)]
        public async Task GetFlightByFlightNumber_Failure_ArgumentException_400(int flightNumber)
        {
            Mock<FlightService> service = new Mock<FlightService>();
            service.Setup(s => s.GetFlightByFlightNumber(1)).Throws(new ArgumentException());

            FlightController controller = new FlightController(service.Object);
            ObjectResult response = await controller.GetFlightByFlightNumber(flightNumber) as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("Bad request", response.Value);
        }

        [TestMethod]
        public async Task GetFlightByFlightNumber_Failure_FlightNotFoundException_404()
        {
            Mock<FlightService> service = new Mock<FlightService>();
            service.Setup(s => s.GetFlightByFlightNumber(1)).Throws(new FlightNotFoundException());

            FlightController controller = new FlightController(service.Object);
            ObjectResult response = await controller.GetFlightByFlightNumber(1) as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("The flight was not found in the database", response.Value);
        }
    }
}
