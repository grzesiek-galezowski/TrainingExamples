namespace NullAsNothing
{
    class Program
    {
        static void Main(string[] args)
        {
            var mySystem = new MySystem(
                new UsersCache(), 
                new RadioCache(), 
                new GroupCache());
            
            var result = mySystem.QueryWith(WhateverQuery());
            result.SendToUser();
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
