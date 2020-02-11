using System.Collections.Generic;

namespace NUnitTestProject1
{
    public class Surname : Value.ValueType<Surname>
    {
        private readonly string _value;

        private Surname(string value)
        {
            _value = value;
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            yield return _value;
        }

        public static Surname Value(string value)
        {
            return new Surname(value);
        }

        public override string ToString()
        {
          return _value;
        }

    }
}