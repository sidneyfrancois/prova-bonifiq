using Microsoft.EntityFrameworkCore;
using Moq;
using ProvaPub.Controllers;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using System.Collections;
using Xunit;

namespace ProvaPub.Tests
{
    public class Parte2Controller_Test
    {
        [Theory(DisplayName = "Teste de ListProducts")]
        [Trait("Parte2Controller", "Parte2 Mock Test")]
        [InlineData(1)]
        [InlineData(2)]
        public void Parte2Controller_ListProducts_ReturnSucess(int page)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductListDatabase")
                .Options;

            var contextDb = new TestDbContext(options);
            var products = contextDb.getProductSeed();
            contextDb.Products.AddRange(products);
            contextDb.SaveChanges();
            var productService = new ProductService(contextDb);

            // Act
            ProductList listResult = productService.ListProducts(page);

            // Assert
            Assert.Equal(page*10, listResult.Products.Count);
        }
    }
}
