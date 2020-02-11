using System.Collections.Generic;

namespace NUnitTestProject1
{
    public class Name : Value.ValueType<Name>
    {
        private readonly string _value;

        private Name(string value)
        {
            _value = value;
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            yield return _value;
        }

        public static Name Value(string value)
        {
            return new Name(value);
        }

        public override string ToString()
        {
          return _value;
        }
    }
}