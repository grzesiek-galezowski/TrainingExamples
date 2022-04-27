namespace NullAsNothing;

class Program
{
    static void Main(string[] args)
    {
        var mainCache = new MainCache(
            new UsersCache(), 
            new RadioCache(), 
            new GroupCache());
            
        var result = mainCache.QueryWith(QueryForRadio("radio1"));

        //if the null check wasn't here, it would be hard to
        //track where the NullReferenceException came from...
        if (result != null)
        {
            result.SendToUser();
        }
    }

    private static QueryForData QueryForRadio(string entityId)
    {
        return new QueryForData(EntityTypes.Radio, entityId);
    }
}

internal record QueryForData(
    EntityTypes EntityType, 
    string EntityId);