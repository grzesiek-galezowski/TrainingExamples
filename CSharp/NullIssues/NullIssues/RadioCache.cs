using Functional.Maybe;

namespace NullAsNothingRefactored2
{
    internal class RadioCache : ICache
    {
        public Maybe<QueryResult> GetBy(string entityId)
        {
            return ((QueryResult) null).ToMaybe();
        }
    }
}