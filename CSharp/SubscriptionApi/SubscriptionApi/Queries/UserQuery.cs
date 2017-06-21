using System.Collections.Generic;
using System.Linq;
using SubscriptionApi.Authorization;

namespace SubscriptionApi.Queries
{
  public class UserQuery : AssetQuery
  {
    private readonly string _name;
    private AssetQueryResolution _authorizationStructure;

    public UserQuery(string name, AssetQueryResolution authorizationStructure)
    {
      _name = name;
      _authorizationStructure = authorizationStructure;
    }

    public void ResolveInto(List<string> requestedAssetNames, QueryResolutionEvents resolutionEvents)
    {
      var assetsFromQuery = _authorizationStructure.RetrieveAssetsByUserName(_name);
      if (!assetsFromQuery.Any())
      {
        resolutionEvents.NoResolutionResultsFor(_name);
      }
      else
      {
        requestedAssetNames.AddRange(assetsFromQuery);
      }
    }
  }
}