using System;

namespace NullAsNothingRefactored2
{
    internal class UnknownEntityTypeException : Exception
    {
        public UnknownEntityTypeException(object entityType, object entityId)
        {
            
        }
    }
}