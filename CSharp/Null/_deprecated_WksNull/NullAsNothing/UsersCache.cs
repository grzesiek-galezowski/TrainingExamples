﻿namespace NullAsNothing
{
    internal class UsersCache : ICache
    {
        public QueryResult GetBy(string entityId)
        {
            return new QueryResult();
        }
    }
}