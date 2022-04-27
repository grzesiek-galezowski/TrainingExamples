namespace NullAsNothing;

internal interface ICache
{
    QueryResult GetBy(string entityId);
}