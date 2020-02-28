using System;

namespace ParseNotValidate.Values
{
    public class EmptySurnameException : Exception
    {
        public EmptySurnameException()
            : base("Surname cannot be an empty string")    
        {
        }

    }
}