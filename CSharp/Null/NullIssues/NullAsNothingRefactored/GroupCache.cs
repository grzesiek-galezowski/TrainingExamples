using Core.Maybe;

namespace NullAsNothingRefactored;

internal class GroupCache : ICache
{
    public Maybe<QueryResult> GetBy(string entityId)
    {
        return new QueryResult().ToMaybe();
    }
}