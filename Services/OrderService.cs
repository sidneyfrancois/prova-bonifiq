using ProvaPub.Models;
using ProvaPub.SmartEnum;

namespace ProvaPub.Services
{
	public class OrderService
	{
        public async Task<Order> PayOrder(PaymentType paymentMethod, decimal paymentValue, int customerId)
		{
			// get paymentMethod based on the string value
			//var paymentT = PaymentType.FromValue((int)paymentMethod);

			// do the payment using the method of the specified payment method
			var result = paymentMethod.FazerPagamento(paymentValue);

			return await Task.FromResult( new Order()
			{
				Value = result
			});
		}
	}
}
