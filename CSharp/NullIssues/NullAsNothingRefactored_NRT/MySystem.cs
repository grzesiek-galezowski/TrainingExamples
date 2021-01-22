namespace NullAsNothingRefactored_NRT
{
    internal class MySystem
    {
        private readonly ICache _usersCache;
        private readonly ICache _radioCache;
        private readonly ICache _groupCache;

        public MySystem(ICache usersCache, ICache radioCache, ICache groupCache)
        {
            _usersCache = usersCache;
            _radioCache = radioCache;
            _groupCache = groupCache;
        }

        public QueryResult? QueryWith(QueryForData queryForData)
        {
            if (queryForData.EntityType == EntityTypes.User)
            {
                return _usersCache.GetBy(queryForData.EntityId);
            }
            else if (queryForData.EntityType == EntityTypes.Radio)
            {
                return _radioCache.GetBy(queryForData.EntityId);
            }
            else if (queryForData.EntityType == EntityTypes.Group)
            {
                return _groupCache.GetBy(queryForData.EntityId);
            }
            else
            {
                throw new UnknownEntityTypeException(queryForData.EntityType, queryForData.EntityId);
            }
        }
    }
}