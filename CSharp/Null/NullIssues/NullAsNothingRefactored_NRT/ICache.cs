namespace NullAsNothingRefactored_NRT;

internal interface ICache
{
    QueryResult? GetBy(string entityId);
}