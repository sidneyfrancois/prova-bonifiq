using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class CustomerService
    {
        TestDbContext _ctx;

        public CustomerService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<Customer> ListCustomers(int page)
        {
            var customerPageList = new PageList<Customer>();

            customerPageList.ResultPageList = _ctx.Customers.Skip((page - 1) * customerPageList.TotalCount).Take(customerPageList.TotalCount).ToList();

            return customerPageList.ResultPageList;
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            await CheckCustomerExistence(customerId);

            //Business Rule: A customer can purchase only a single time per month
            await CheckPurchaseValidation(customerId);

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            await CheckCustomerIsFirstPurchase(customerId, purchaseValue);

            return true;
        }

        private async Task CheckCustomerIsFirstPurchase(int customerId, decimal purchaseValue)
        {
            var haveBoughtBefore = await _ctx.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                throw new InvalidOperationException($"Customer {customerId} can make a first purchase of maximum 100,00");
        }

        private async Task CheckPurchaseValidation(int customerId)
        {
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await _ctx.Orders.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            if (ordersInThisMonth > 0)
                throw new InvalidOperationException($"Customer {customerId} can't make more purchases in this mounth (only one purchase per mounth");
        }

        private async Task CheckCustomerExistence(int customerId)
        {
            var customer = await _ctx.Customers.FindAsync(customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");
        }

    }
}
