using FlyingDutchmanAirlines.RepositoryLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlyingDutchmanAirlines.DatabaseLayer;

namespace FlyingDutchmanAirlines_Tests.RepositoryLayer
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        [TestMethod]
        public async Task CreateCustomer_Success()
        {
            bool result = await _repository.CreateCustomer("Donald Knuth");
            Assert.IsTrue(result);
        }
        [TestMethod]
        public async Task CreateCustomer_Failure_NameIsNull()
        {
            bool result = await _repository.CreateCustomer(null);
            Assert.IsFalse(result);
        }

        [TestMethod] 
        public async Task CreateCustomer_Failure_NameIsEmptyString()
        {
            bool result = await _repository.CreateCustomer("");
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow('#')]
        [DataRow('$')]
        [DataRow('%')]
        [DataRow('&')]
        [DataRow('*')]
        public async Task CreateCustomer_Failure_NameContainsInvalidCharacters(char invalidCharacter)
        {
            bool result = await _repository.CreateCustomer("Donald Knuth" + invalidCharacter);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CreateCustomer_Failure_DatabaseAccessError()
        {
            CustomerRepository repository = new CustomerRepository(null);
            Assert.IsNotNull(repository);

            bool result = await repository.CreateCustomer("Donald Knuth");
            Assert.IsFalse(result);
        }

        private FlyingDutchmanAirlinesContext _context;
        private CustomerRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            DbContextOptions<FlyingDutchmanAirlinesContext> dbContextOptions = new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>().UseInMemoryDatabase("FlyingDutchman").Options;
            _context = new FlyingDutchmanAirlinesContext(dbContextOptions);
            _repository = new CustomerRepository(_context);
            Assert.IsNotNull(_repository);
        }
    }
}

