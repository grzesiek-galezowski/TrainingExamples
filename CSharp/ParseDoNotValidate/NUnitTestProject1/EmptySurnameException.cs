using System;

namespace NUnitTestProject1
{
    public class EmptySurnameException : Exception
    {
        public EmptySurnameException()
            : base("Surname cannot be an empty string")    
        {
        }

    }
}