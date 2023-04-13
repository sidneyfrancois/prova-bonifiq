namespace ProvaPub.SmartEnum
{
    public abstract class PaymentType : Enumeration<PaymentType>
    {
        private PaymentType(int value, string displayName) : base(value, displayName) { }
        private PaymentType() { }

        public static readonly PaymentType Pix = new PixPayment();
        public static readonly PaymentType CreditCard = new CreditCardPayment();
        public static readonly PaymentType Paypal = new PaypalPayment();

        private sealed class PixPayment : PaymentType
        {
            public PixPayment() : base(0, "Pix") { }
        }

        private sealed class CreditCardPayment : PaymentType
        {
            public CreditCardPayment() : base(1, "Cartão de crédito") { }
        }

        private sealed class PaypalPayment: PaymentType
        {
            public PaypalPayment() : base(2, "Paypal") { }
        }
    }
}
