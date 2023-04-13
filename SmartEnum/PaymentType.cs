using ProvaPub.SmartEnum.PaymentMethods;

namespace ProvaPub.SmartEnum
{
    public abstract class PaymentType : Enumeration<PaymentType>
    {
        protected PaymentType(int value, string displayName) : base(value, displayName) { }

        public static readonly PaymentType Pix = new PaymentByPix();
        public static readonly PaymentType CreditCard = new PaymentByCreditCard();
        public static readonly PaymentType Paypal = new PaymentByPaypal();

        // portuguese name only for display purposes
        public abstract decimal FazerPagamento(decimal paymentValue);
    }
}
