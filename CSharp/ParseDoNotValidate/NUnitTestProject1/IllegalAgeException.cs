using System;

namespace NUnitTestProject1
{
    public class IllegalAgeException : Exception
    {
        public IllegalAgeException(string reason)
            : base($"Age cannot be {reason}")
        {
            
        }
    }
}