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

            QueryResult? maybeResult = mySystem.QueryWith(WhateverQuery());

            if (maybeResult != null)
            {
                maybeResult.SendToUser();
            }
        }

        private static QueryForData WhateverQuery()
        {
            return new QueryForData(EntityTypes.Radio, "trolololo");
        }
    }

    internal record QueryForData(EntityTypes EntityType, string EntityId)
    {
    }
}
