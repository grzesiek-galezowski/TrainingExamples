using System;

namespace NUnitTestProject1
{
    public class EmptyNameException : Exception
    {
        public EmptyNameException()
            : base("Name cannot be an empty string")    
        {
        }

    }
}