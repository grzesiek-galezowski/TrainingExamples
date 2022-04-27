using Core.Maybe;

namespace NullAsNothingRefactored;

internal interface ICache
{
    Maybe<QueryResult> GetBy(string entityId);
}