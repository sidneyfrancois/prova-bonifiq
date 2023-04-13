namespace ProvaPub.SmartEnum.PaymentMethods
{
    public sealed class PaymentByPix : PaymentType
    {
        public PaymentByPix() : base(0, "Pix") { }

        public override decimal FazerPagamento(decimal paymentValue)
        {
            return paymentValue + 20;
        }
    }
}
