﻿namespace NullAsNothing
{
    internal class GroupCache : ICache
    {
        public QueryResult GetBy(string entityId)
        {
            return new QueryResult();
        }
    }
}