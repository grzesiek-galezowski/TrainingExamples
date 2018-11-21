using Functional.Maybe;

namespace NullAsNothingRefactored2
{
    public interface ICache
    {
        Maybe<QueryResult> GetBy(string entityId);
    }
}