using ProvaPub.Models;

namespace ProvaPub.Services
{
	public class OrderService
	{
		public async Task<Order> PayOrder(PaymentType paymentMethod, decimal paymentValue, int customerId)
		{
			if (paymentMethod == PaymentType.Pix)
			{
				//Faz pagamento...
			}
			else if (paymentMethod == PaymentType.CreditCard)
			{
				//Faz pagamento...
			}
			else if (paymentMethod == PaymentType.CreditCard)
			{
				//Faz pagamento...
			}

			return await Task.FromResult( new Order()
			{
				Value = paymentValue
			});
		}
	}
}
