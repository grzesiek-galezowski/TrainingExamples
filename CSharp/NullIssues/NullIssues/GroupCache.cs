using Functional.Maybe;

namespace NullAsNothingRefactored2
{
    internal class GroupCache : ICache
    {
        public Maybe<QueryResult> GetBy(string entityId)
        {
            return new QueryResult().ToMaybe();
        }
    }
}