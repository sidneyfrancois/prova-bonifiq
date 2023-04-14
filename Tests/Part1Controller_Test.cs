using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class Part1Controller_Test
    {
        private RandomService FactoryRandomService() => new RandomService();


        [Fact(DisplayName = "RandomService Válido")]
        [Trait("Parte1Controller", "RandomService Test")]
        public void FactoryRandomService_Create_SameTypeAsRandomService()
        {
            var randomService = FactoryRandomService();
            Assert.IsType<RandomService>(randomService);
        }

        [Fact(DisplayName = "Válidade de aleatoriedade")]
        [Trait("Parte1Controller", "Random Service Test")]
        public void RandomService_GetRandom_CheckRandomnessReturn()
        {
            // Arrange

            // Act 
            var randomNumbers = Enumerable.Range(0, 10)
                .Select(i => FactoryRandomService().GetRandom())
                .ToList();

            // Assert
            Assert.Equal(randomNumbers.Count, randomNumbers.Distinct().Count());
        }
    }
}
