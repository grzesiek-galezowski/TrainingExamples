﻿using Core.Maybe;

namespace NullAsNothingRefactored;

class Program
{
    static void Main(string[] args)
    {
        var mainCache = new MainCache(
            new UsersCache(), 
            new RadioCache(), 
            new GroupCache());

        Maybe<QueryResult> maybeResult = mainCache.QueryWith(QueryForRadio("radio1"));

        if (maybeResult.HasValue)
        {
            maybeResult.Value().SendToUser();
        }
    }

    private static QueryForData QueryForRadio(string entityId)
    {
        return new QueryForData(EntityTypes.Radio, entityId);
    }
}

internal record QueryForData(
    EntityTypes EntityType, 
    string EntityId);