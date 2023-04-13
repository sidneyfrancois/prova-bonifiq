using System.Reflection;

namespace ProvaPub.SmartEnum
{
    public abstract class Enumeration<TEnum> where TEnum : Enumeration<TEnum>
    {
        private readonly int _value;
        private readonly string _displayName;

        protected Enumeration()
        {
        }

        protected Enumeration(int value, string displayName)
        {
            _value = value;
            _displayName = displayName;
        }

        public int Value
        {
            get { return _value; }
        }

        public string DisplayName
        {
            get { return _displayName; }
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public static IEnumerable<T> GetAll<T>()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var locatedValue = info;

                if (type.IsAssignableFrom(locatedValue.FieldType) && locatedValue != null)
                {
                    yield return (T)locatedValue.GetValue(default);
                }
            }
        }

        public static TEnum FromValue(int value)
        {
            var matchingItem = parse<TEnum, int>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        private static T parse<T, K>(K value, string description, Func<T, bool> predicate)
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }
    }
}
