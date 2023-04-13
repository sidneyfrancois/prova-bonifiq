using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class Part1Controller_Test
    {
        private RandomService FactoryRandomService() => new RandomService();

        [Fact]
        public void RandomService_GetRandom_CheckRandomnessReturn()
        {
            // Arrange

            // Act 
            var randomNumbers = Enumerable.Range(0, 20)
                .Select(i => FactoryRandomService().GetRandom())
                .ToList();

            // Assert
            Assert.Equal(randomNumbers.Count, randomNumbers.Distinct().Count());
        }
    }
}
