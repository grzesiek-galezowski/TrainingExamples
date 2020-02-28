using System;

namespace ParseNotValidate.Values
{
    public class IllegalAgeException : Exception
    {
        public IllegalAgeException(string reason)
            : base($"Age cannot be {reason}")
        {
            
        }
    }
}