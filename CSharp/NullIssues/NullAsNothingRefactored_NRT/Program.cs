using System;

namespace NullAsNothingRefactored_NRT
{
    class Program
    {
        static void Main(string[] args)
        {
            var mySystem = new MySystem(
                new UsersCache(), 
                new RadioCache(), 
                new GroupCache());

            QueryResult? maybeResult = mySystem.QueryWith(RadioQuery());

            if (maybeResult != null)
            {
                maybeResult.SendToUser();
            }
        }

        private static QueryForData RadioQuery()
        {
            return new QueryForData(EntityTypes.Radio, "trolololo");
        }
    }

    internal record QueryForData(EntityTypes EntityType, string EntityId)
    {
    }
}
