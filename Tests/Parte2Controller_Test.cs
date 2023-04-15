using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.Frameworks;
using ProvaPub.Controllers;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using System.Collections;
using Xunit;

namespace ProvaPub.Tests
{
    [Collection(nameof(DbContextTest))]
    public class Parte2Controller_Test
    {
        private readonly Parte2Controller_Fixture_Test _productFixture;

        public Parte2Controller_Test(Parte2Controller_Fixture_Test productFixture)
        {
            _productFixture = productFixture;
        }

        [Theory(DisplayName = "Teste de ListProducts")]
        [Trait("Parte2Controller", "Parte2 Mock Test")]
        [InlineData(1)]
        [InlineData(2)]
        public void Parte2Controller_ListProducts_ReturnSucess(int page)
        {
            // Arrange
            var productService = new ProductService(_productFixture._context);

            // Act
            var listResult = productService.ListProducts(page);

            // Assert
            Assert.Equal(10, listResult.Count);
            Assert.Equal(((page*10)), listResult.Last().Id);
        }
    }
}
