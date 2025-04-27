using System;
using System.Collections.Generic;
using System.Linq;

namespace AspectsOfImmutability
{
    // Assuming equality and hashcode are implemented correctly,
    // are objects of this class immutable?
    public class AmIImmutable
    {
        private readonly IReadOnlyList<int> _numbers;

        public AmIImmutable(IReadOnlyList<int> numbers)
        {
            _numbers = numbers;
        }

        public int First() => _numbers.Single();

        private bool Equals(AmIImmutable other)
        {
            return _numbers.SequenceEqual(other._numbers);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AmIImmutable) obj);
        }

        public override int GetHashCode()
        {
            return (_numbers != null ? _numbers.GetHashCode() : 0);
        }
    }
}
