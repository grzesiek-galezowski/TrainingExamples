using System;

namespace NullAsNothingRefactored
{
    internal class UnknownEntityTypeException : Exception
    {
        public UnknownEntityTypeException(object entityType, object entityId)
        {
            
        }
    }
}