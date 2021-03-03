using System;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;

namespace FlyingDutchmanAirlines.ServiceLayer
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        private readonly CustomerRepository _customerRepository;

        public BookingService(BookingRepository bookingRepository, CustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
        }

        public async Task<(bool, Exception)> CreateBooking(string name, int flightNumber)
        {
            try
            {
                Customer customer;
                try
                {
                    customer = await _customerRepository.GetCustomerByName(name);
                }
                catch (FlightNotFoundException)
                {
                    await _customerRepository.CreateCustomer(name);
                    return await CreateBooking(name, flightNumber);
                }

                await _bookingRepository.CreateBooking(customer.CustomerId, flightNumber);
                return (true, null);
            }
            catch (Exception exception)
            {
                return (false, exception);
            }
        }
    }
}
