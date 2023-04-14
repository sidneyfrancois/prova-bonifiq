using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using Xunit;


namespace ProvaPub.Tests
{
    [CollectionDefinition(nameof(ProductContext))]
    public class ProductContext: ICollectionFixture<Parte2Controller_Fixture_Test>
    {}


    public class Parte2Controller_Fixture_Test : IDisposable
    {
        public TestDbContext productsContext { get; set; }

        public Parte2Controller_Fixture_Test()
        {
            productsContext = GenerateContext();
        }

        public TestDbContext GenerateContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductListDatabase")
                .Options;

            var contextDb = new TestDbContext(options);
            contextDb.Products.AddRange(contextDb.getProductSeed());
            contextDb.SaveChanges();
            return contextDb;
        }

        public void Dispose()
        {

        }
    }
}
