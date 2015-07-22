using System.Collections.Generic;
using System.Linq;
using SubscriptionApi.Authorization;

namespace SubscriptionApi.Queries
{
  public class HardwareQuery : AssetQuery
  {
    private readonly string _name;
    private readonly AssetQueryResolution _authorizationStructure;

    public HardwareQuery(string name, AssetQueryResolution authorizationStructure)
    {
      _name = name;
      _authorizationStructure = authorizationStructure;
    }

    public void ResolveInto(List<string> requestedAssetNames, QueryResolutionEvents resolutionEvents)
    {
      var assetsFromQuery = _authorizationStructure.RetrieveAssetsByHardwareName(_name);
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