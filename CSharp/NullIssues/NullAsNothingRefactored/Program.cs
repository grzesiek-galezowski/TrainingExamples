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

            Maybe<QueryResult> result = mySystem.QueryWith(WhateverQuery());
            if (result.HasValue)
            {
                result.Value.SendToUser();
            }
        }

        private static QueryForData WhateverQuery()
        {
            return new QueryForData()
            {
                EntityId = "trolololo",
                EntityType = EntityTypes.Radio
            };
        }
    }

    internal class QueryForData
    {
        public EntityTypes EntityType { get; set; }
        public string EntityId { get; set; }
    }
}
