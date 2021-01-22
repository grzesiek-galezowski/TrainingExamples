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
            
            var result = mySystem.QueryWith(RadioQuery());

            if (result != null)
            {
                result.SendToUser();
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
