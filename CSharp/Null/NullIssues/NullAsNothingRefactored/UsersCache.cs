using Core.Maybe;

namespace NullAsNothingRefactored;

public class UsersCache : ICache
{
    public Maybe<QueryResult> GetBy(string entityId)
    {
        return new QueryResult().ToMaybe();
    }
}