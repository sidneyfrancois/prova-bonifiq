namespace ProvaPub.SmartEnum
{
    public abstract class PaymentType : Enumeration<PaymentType>
    {
        private PaymentType(int value, string displayName) : base(value, displayName) { }
        private PaymentType() { }

        public static readonly PaymentType Pix = new PixPayment();
        public static readonly PaymentType CreditCard = new CreditCardPayment();
        public static readonly PaymentType Paypal = new PaypalPayment();

        // portuguese name only for display purposes
        public abstract decimal FazerPagamento(decimal paymentValue);

        private sealed class PixPayment : PaymentType
        {
            public PixPayment() : base(0, "Pix") { }

            public override decimal FazerPagamento(decimal paymentValue)
            {
                return paymentValue + 20;
            }
        }

        private sealed class CreditCardPayment : PaymentType
        {
            public CreditCardPayment() : base(1, "Cartão de crédito") { }

            public override decimal FazerPagamento(decimal paymentValue)
            {
                return paymentValue + 30;
            }
        }

        private sealed class PaypalPayment: PaymentType
        {
            public PaypalPayment() : base(2, "Paypal") { }

            public override decimal FazerPagamento(decimal paymentValue)
            {
                return paymentValue + 40;
            }
        }
    }
}
