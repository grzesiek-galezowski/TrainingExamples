using System.Collections.Generic;

namespace NUnitTestProject1
{
    public class Age : Value.ValueType<Age>
    {
        private readonly int _value;
        private Age(in int value) => _value = value;
        public int Value => _value;
        public static Age Of(int value) => new Age(value);
        public override string ToString() => _value.ToString();

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            yield return _value;
        }

    }
}