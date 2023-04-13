namespace ProvaPub.SmartEnum.PaymentMethods
{
    public class PaymentByPaypal : PaymentType
    {
        public PaymentByPaypal() : base(2, "Paypal") { }

        public override decimal FazerPagamento(decimal paymentValue)
        {
            return paymentValue + 40;
        }
    }
}
