using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class FlightRepository
    {
        private readonly FlyingDutchmanAirlinesContext _context;
        public FlightRepository(FlyingDutchmanAirlinesContext _context)
        {
            this._context = _context;
        }
        public async Task<Flight> GetFlightByFlightNumber(int flightNumber, int originAirportId, int destinationAirportId)
        {
            if (flightNumber < 0)
            {
                Console.WriteLine($"Could not find flight in GetFlightByFlightNumber! flightNumber = {flightNumber}");
                throw new FlightNotFoundException();
            }

            if (!originAirportId.IsPositiveInteger() || !destinationAirportId.IsPositiveInteger())
            {
                Console.WriteLine($"Argument Exception in GetFlightByFlightNumber! originAirportId = {originAirportId} : destinationAirportId = {destinationAirportId}");
                throw new ArgumentException("invalid arguments provided");
            }

            return await _context.Flight.FirstOrDefaultAsync(f => f.FlightNumber == flightNumber) ??
                   throw new FlightNotFoundException();
        }
    }
}
