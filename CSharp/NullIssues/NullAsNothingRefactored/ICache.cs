namespace NullAsNothingRefactored
{
    internal interface ICache
    {
        QueryResult GetBy(string entityId);
    }
}