using System;
using System.Collections.Generic;

namespace NUnitTestProject1
{
    public class Surname : Value.ValueType<Surname>
    {
        private readonly string _value;

        public static Surname Value(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            if(value == string.Empty)
            {
                throw new EmptyNameException();
            }

            return new Surname(value);
        }

        private Surname(string value) => _value = value;
        public override string ToString() => _value;

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            yield return _value;
        }



    }
}