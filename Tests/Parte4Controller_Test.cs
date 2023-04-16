﻿using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using ProvaPub.Models;
using ProvaPub.Services;
using System;
using System.Collections.Generic;
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

        public void MockOrder(int customerId)
        {
            var newOrder = new Order()
            {
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow.AddHours(-1),
            };

            _fixtureContext._context.Orders.Add(newOrder);

            _fixtureContext._context.SaveChanges();
        }

        public void RemoveMockOrder(int customerId)
        {
            _fixtureContext._context.Customers.Find(customerId).Orders = null;
            _fixtureContext._context.SaveChanges();
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

        [Theory(DisplayName = "Test exception if customer does not exist")]
        [Trait("Parte4Controller", "Parte4 Business Rule Test")]
        [InlineData(23)]
        [InlineData(25)]
        [InlineData(50)]
        [InlineData(40)]
        [InlineData(100)]
        public async void Parte4Controller_CheckCustomerExistence_ReturnException(int customerId)
        {
            // Arrange
            var customerService = new CustomerService(_fixtureContext._context);

            // Act
            Func<Task> result = () => customerService.CheckCustomerExistence(customerId);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            // Assert
            Assert.Equal($"Customer Id {customerId} does not exists", exception.Message);
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

        [Theory(DisplayName = "Test exception if customer make more than one purchase in the mounth")]
        [Trait("Parte4Controller", "Parte4 Business Rule Test")]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(1)]
        [InlineData(20)]
        [InlineData(15)]
        public async void Parte4Controller_CheckPurchaseValidation_ReturnException(int customerId)
        {
            // Arrange
            var customerService = new CustomerService(_fixtureContext._context);
            var customer = _fixtureContext._context.Customers.Find(customerId);
            MockOrder(customerId);

            // Act
            Func<Task> result = () => customerService.CheckPurchaseValidation(customerId);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            // Assert
            Assert.Equal($"Customer {customerId} can't make more purchases in this mounth (only one purchase per mounth", exception.Message);
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

        [Theory(DisplayName = "Test exception if is not the first purchase and if purchase's value is bigger than 100")]
        [Trait("Parte4Controller", "Parte4 Business Rule Test")]
        [InlineData(2, 101)]
        [InlineData(5, 120)]
        [InlineData(1, 180)]
        [InlineData(20, 150)]
        [InlineData(15, 140)]
        public async void Parte4Controller_CheckFirstPurchase_ReturnException(int customerId, decimal purchaseValue)
        {
            // Arrange
            var customerService = new CustomerService(_fixtureContext._context);
            RemoveMockOrder(customerId);

            // Act
            Func<Task> result = () => customerService.CheckCustomerIsFirstPurchase(customerId, purchaseValue);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(result);

            // Assert
            Assert.Equal($"Customer {customerId} can make a first purchase of maximum 100,00", exception.Message);
        }

        [Theory(DisplayName = "Test if customer can purchase")]
        [Trait("Parte4Controller", "Parte4 Business Rule Test")]
        [InlineData(2, 100)]
        [InlineData(5, 20)]
        [InlineData(1, 80)]
        [InlineData(20, 50)]
        [InlineData(15, 40)]
        public void Parte4Controller_CanPurchase_CompleteTaskSuccessfully(int customerId, decimal purchaseValue)
        {
            // Arrange
            var customerService = new CustomerService(_fixtureContext._context);

            // Act
            Task result = customerService.CanPurchase(customerId, purchaseValue);
            result.Wait();

            // Assert
            Assert.True(result.IsCompletedSuccessfully);
        }
    }
}
