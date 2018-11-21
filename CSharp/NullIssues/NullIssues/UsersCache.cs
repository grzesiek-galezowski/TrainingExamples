using Functional.Maybe;

namespace NullAsNothingRefactored2
{
    public class UsersCache : ICache
    {
        public Maybe<QueryResult> GetBy(string entityId)
        {
            return new QueryResult().ToMaybe();
        }
    }
}