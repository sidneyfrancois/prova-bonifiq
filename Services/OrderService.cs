using ProvaPub.Models;
using ProvaPub.SmartEnum;

namespace ProvaPub.Services
{
	public class OrderService
	{
		public async Task<Order> PayOrder(PaymentTypeEnum paymentMethod, decimal paymentValue, int customerId)
		{
			var paymentT = PaymentType.FromValue((int)paymentMethod);
			var result = paymentT.FazerPagamento(paymentValue);



			return await Task.FromResult( new Order()
			{
				Value = result
			});
		}
	}
}
