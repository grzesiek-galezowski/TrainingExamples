namespace NullAsNothingRefactored_NRT;

public class UsersCache : ICache
{
    public QueryResult? GetBy(string entityId)
    {
        return new QueryResult();
    }
}