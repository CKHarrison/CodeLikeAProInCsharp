using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class CustomerRepository
    {
        private readonly FlyingDutchmanAirlinesContext _context;

        public CustomerRepository(FlyingDutchmanAirlinesContext _context)
        {
            this._context = _context;
        }
        public async Task<bool> CreateCustomer(string name)
        {
            if (IsInvalidCustomerName(name))
            {
                return false;
            }

            try
            {
                Customer newCustomer = new Customer(name);
                using (_context)
                {
                    _context.Customers.Add(newCustomer);
                    await _context.SaveChangesAsync();
                }
            } catch
            {
                return false;
            }
           

            return true;
        }

        public bool IsInvalidCustomerName(string name)
        {
            char[] forbiddenCharacters = { '!', '@', '#', '$', '%', '&', '*' };
            return string.IsNullOrEmpty(name) || name.Any(x => forbiddenCharacters.Contains(x));
        }
    }
}
