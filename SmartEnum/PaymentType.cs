namespace ProvaPub.SmartEnum
{
    public abstract class PaymentType : Enumeration<PaymentType>
    {
        private PaymentType(int value, string displayName) : base(value, displayName) { }
        private PaymentType() { }
    }
}
