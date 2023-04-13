namespace ProvaPub.SmartEnum
{
    public class PaymentType : Enumeration
    {
        private PaymentType(int value, string displayName) : base(value, displayName) { }
        private PaymentType() { }

        public static readonly PaymentType Pix = new PaymentType(0, "Pix");
        public static readonly PaymentType CreditCard = new PaymentType(1, "Cartão de crédito");
        public static readonly PaymentType Paypal = new PaymentType(2, "Paypal");
    }
}
