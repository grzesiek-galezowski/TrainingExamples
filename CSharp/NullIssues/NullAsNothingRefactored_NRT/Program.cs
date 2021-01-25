using System;

namespace NullAsNothingRefactored_NRT
{
    class Program
    {
        static void Main(string[] args)
        {
            var mySystem = new MainCache(
                new UsersCache(), 
                new RadioCache(), 
                new GroupCache());

            QueryResult? maybeResult = mySystem.QueryWith(QueryForRadio("radio1"));

            if (maybeResult != null)
            {
                maybeResult.SendToUser();
            }
        }

        private static QueryForData QueryForRadio(string entityId)
        {
            return new QueryForData(EntityTypes.Radio, entityId);
        }
    }

    internal record QueryForData(EntityTypes EntityType, string EntityId)
    {
    }
}
