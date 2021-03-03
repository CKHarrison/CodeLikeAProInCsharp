using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.ServiceLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.DatabaseLayer.Models;

namespace FlyingDutchmanAirlines_Tests.ServiceLayer
{
    [TestClass]
    public class BookingServiceTests
    {
        [TestMethod]
        public async Task CreateBooking_Success()
        {
            Mock<BookingRepository> mockBookingRepository = new Mock<BookingRepository>();
            Mock<CustomerRepository> mockCustomerRepository = new Mock<CustomerRepository>();
            
            mockBookingRepository.Setup(repository => repository.CreateBooking(0, 0)).Returns(Task.CompletedTask);
            mockCustomerRepository.Setup(repository => repository.GetCustomerByName("Leo Tolstoy"))
                .Returns(Task.FromResult(new Customer("Leo Tolstoy")));

            BookingService service = new BookingService(mockBookingRepository.Object, mockCustomerRepository.Object);

            (bool result, Exception exception) = await service.CreateBooking("Leo Tolstoy", 0);

            Assert.IsTrue(result);
            Assert.IsNull(exception);
        }
    }
}
