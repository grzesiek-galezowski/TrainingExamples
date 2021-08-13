using Functional.Maybe;

namespace NullAsNothingRefactored
{
    internal interface ICache
    {
        Maybe<QueryResult> GetBy(string entityId);
    }
}