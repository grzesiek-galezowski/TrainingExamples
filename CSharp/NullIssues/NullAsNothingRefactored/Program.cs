using Functional.Maybe;

namespace NullAsNothingRefactored
{
    class Program
    {
        static void Main(string[] args)
        {
            var mySystem = new MySystem(
                new UsersCache(), 
                new RadioCache(), 
                new GroupCache());

            Maybe<QueryResult> maybeResult = mySystem.QueryWith(RadioQuery());

            if (maybeResult.HasValue)
            {
                maybeResult.Value.SendToUser();
            }
        }

        private static QueryForData RadioQuery()
        {
            return new QueryForData(EntityTypes.Radio, "trolololo");
        }
    }

    internal record QueryForData(
        EntityTypes EntityType, 
        string EntityId) { }
}
