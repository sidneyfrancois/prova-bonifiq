using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using Xunit;


namespace ProvaPub.Tests
{
    [CollectionDefinition(nameof(DbContextTest))]
    public class DbContextTest: ICollectionFixture<Context_Fixture_Test>
    {}


    public class Context_Fixture_Test : IDisposable
    {
        public TestDbContext _context { get; set; }

        public Context_Fixture_Test()
        {
            _context = GenerateContext();
        }

        public TestDbContext GenerateContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "ProvaPubDatabase")
                .Options;

            var contextDb = new TestDbContext(options);

            // Seed data into InMemoryDatabase
            contextDb.Products.AddRange(contextDb.getProductSeed());
            contextDb.Customers.AddRange(contextDb.getCustomerSeed());

            contextDb.SaveChanges();
            return contextDb;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
