using System.Collections.Generic;

namespace ParseNotValidate.Values
{
    public class Age : Value.ValueType<Age>
    {
        public static Age Of(int value)
        {
            if (value < 0)
            {
                throw new IllegalAgeException("a negative value");
            }
            return new Age(value);
        }

        internal Age(int value) => Value = value;
        public int Value { get; }
        public override string ToString() => Value.ToString();

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            yield return Value;
        }

    }
}