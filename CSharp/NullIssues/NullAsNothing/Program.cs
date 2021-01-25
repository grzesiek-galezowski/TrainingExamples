namespace NullAsNothing
{
    class Program
    {
        static void Main(string[] args)
        {
            var mySystem = new MainCache(
                new UsersCache(), 
                new RadioCache(), 
                new GroupCache());
            
            var result = mySystem.QueryWith(QueryForRadio("radio1"));

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
        string EntityId) { }
}
