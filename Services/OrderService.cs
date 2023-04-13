using ProvaPub.Models;
using ProvaPub.SmartEnum;

namespace ProvaPub.Services
{
	public class OrderService
	{
		public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
			// get paymentMethod based on the string value
			var paymentT = PaymentType.FromDisplayName(paymentMethod);

			// do the payment using the method of the specified payment method
			var result = paymentT.FazerPagamento(paymentValue);

			return await Task.FromResult( new Order()
			{
				Value = result
			});
		}
	}
}
