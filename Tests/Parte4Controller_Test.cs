using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using ProvaPub.Services;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace ProvaPub.Tests
{
    [Collection(nameof(DbContextTest))]
    public class Parte4Controller_Test
    {
        private readonly Context_Fixture_Test _fixtureContext;

        public Parte4Controller_Test(Context_Fixture_Test fixtureContext)
        {
            _fixtureContext = fixtureContext;
        }

        [Theory(DisplayName = "Test if customer exists")]
        [Trait("Parte4Controller", "Parte4 Business Rule Test")]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(1)]
        [InlineData(20)]
        [InlineData(15)]
        public void Parte4Controller_CheckCustomerExistence_CompleteTaskSuccessfully(int customerId)
        {
            // Arrange
            var customerService = new CustomerService(_fixtureContext._context);

            // Act
            Task result = customerService.CheckCustomerExistence(customerId);
            result.Wait();

            // Assert
            Assert.True(result.IsCompletedSuccessfully);
        }

        [Theory(DisplayName = "Test if purchase is valid")]
        [Trait("Parte4Controller", "Parte4 Business Rule Test")]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(1)]
        [InlineData(20)]
        [InlineData(15)]
        public void Parte4Controller_CheckPurchaseValidation_CompleteTaskSuccessfully(int customerId)
        {
            // Arrange
            var customerService = new CustomerService(_fixtureContext._context);

            // Act
            Task result = customerService.CheckPurchaseValidation(customerId);
            result.Wait();

            // Assert
            Assert.True(result.IsCompletedSuccessfully);
        }

        [Theory(DisplayName = "Test if purchase is customer's first purchase")]
        [Trait("Parte4Controller", "Parte4 Business Rule Test")]
        [InlineData(2, 100)]
        [InlineData(5, 20)]
        [InlineData(1, 80)]
        [InlineData(20, 50)]
        [InlineData(15, 40)]
        public void Parte4Controller_CheckFirstPurchase_CompleteTaskSuccessfully(int customerId, decimal purchaseValue)
        {
            // Arrange
            var customerService = new CustomerService(_fixtureContext._context);

            // Act
            Task result = customerService.CheckCustomerIsFirstPurchase(customerId, purchaseValue);
            result.Wait();

            // Assert
            Assert.True(result.IsCompletedSuccessfully);
        }
    }
}
