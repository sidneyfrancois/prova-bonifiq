namespace ProvaPub.SmartEnum.PaymentMethods
{
    public class PaymentByCreditCard : PaymentType
    {
        public PaymentByCreditCard() : base(1, "Cartão de Crédito") { }

        public override decimal FazerPagamento(decimal paymentValue)
        {
            return paymentValue + 30;
        }
    }
}
