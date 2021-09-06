using System;

namespace NullAsNothing
{
    internal class UnknownEntityTypeException : Exception
    {
        public UnknownEntityTypeException(object entityType, object entityId)
        {
            
        }
    }
}