using FlyingDutchmanAirlines.DatabaseLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines_Tests.Stubs
{
    class FlyingDutchmanAirlinesContext_Stub : FlyingDutchmanAirlinesContext
    {
        public FlyingDutchmanAirlinesContext_Stub(DbContextOptions<FlyingDutchmanAirlinesContext> options) : base (options)
        {
            base.Database.EnsureDeleted();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            if (base.Booking.Any())
            {
                return base.Booking.First().CustomerId switch
                {
                    1 => 0,
                    _ => throw new Exception("Database Error!")
                };
            }

            if (base.Airport.Any())
            {
                return base.Airport.First().AirportId switch
                {
                    10 => throw new Exception("Database Error!"),
                    _ => 0
                };
            }

            return 0;
        }
    }
}
