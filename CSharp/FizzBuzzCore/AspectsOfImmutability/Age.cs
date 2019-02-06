namespace AspectsOfImmutability
{
    // Assuming equality and hashcode are implemented correctly,
    // can clients that get instance of this class assume they got immutable objects?
    // e.g. public void DoSomething(Age obj) { Assert.AreEqual(obj.GetHashCode(), obj.GetHashCode()); }
    public class Age
    {
        private readonly int _value;

        public Age(int value)
        {
            _value = value;
        }

        private bool Equals(Age other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Age) obj);
        }

        public override int GetHashCode()
        {
            return _value;
        }
    }
}