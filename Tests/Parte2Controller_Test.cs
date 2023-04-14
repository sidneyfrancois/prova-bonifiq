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
        [Fact(DisplayName = "Teste de ListProducts")]
        [Trait("Parte2Controller", "Parte2 Mock Test")]
        public void Parte2Controller_ListProducts_ReturnSucess()
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
            ProductList listResult = productService.ListProducts(10);

            // Assert
            Assert.Equal(10, listResult.Products.Count);
        }
    }
}
