using System;

namespace ParseNotValidate.Values
{
    public class EmptyNameException : Exception
    {
        public EmptyNameException()
            : base("Name cannot be an empty string")    
        {
        }

    }
}