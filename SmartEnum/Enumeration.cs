using System.Reflection;

namespace ProvaPub.SmartEnum
{
    public enum Payment
    {
        Pix,
        Card
    };

    public abstract class Enumeration<TEnum>: IEquatable<Enumeration<TEnum>> where TEnum : Enumeration<TEnum>
    {
        private readonly int _value;
        private readonly string _displayName;

        protected Enumeration(int value, string name)
        {
            _value = value;
            _displayName = name;
        }

        private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();

        public static TEnum? FromValue(int value)
        {
            return Enumerations.TryGetValue(value, out TEnum? enumeration) ? enumeration : default;
        }

        public static TEnum? FromName(string name)
        {
            return Enumerations.Values.SingleOrDefault(e => e._displayName == name);
        }

        public bool Equals(Enumeration<TEnum>? other)
        {
            if (other == null) return false;

            return GetType() == other.GetType() && _value == other._value;

            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return obj is Enumeration<TEnum> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return _displayName;
        }

        private static Dictionary<int, TEnum> CreateEnumerations()
        {
            var enumerationType = typeof(TEnum);

            var fieldsForType = enumerationType
                .GetFields(
                     BindingFlags.Public |
                     BindingFlags.Static |
                     BindingFlags.FlattenHierarchy)
                .Where(fieldInfo =>
                    enumerationType.IsAssignableFrom(fieldInfo.FieldType))
                .Select(fieldInfo =>
                    (TEnum)fieldInfo.GetValue(default)!);

            return fieldsForType.ToDictionary(x => x._value);
        }
    }
}
