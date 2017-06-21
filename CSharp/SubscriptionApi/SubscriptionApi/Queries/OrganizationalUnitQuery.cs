using System.Collections.Generic;
using System.Linq;
using SubscriptionApi.Authorization;

namespace SubscriptionApi.Queries
{
  public class OrganizationalUnitQuery : AssetQuery
  {
    private readonly string _name;
    private readonly AssetQueryResolution _authorizationStructure;

    public OrganizationalUnitQuery(string name, AssetQueryResolution authorizationStructure)
    {
      _name = name;
      _authorizationStructure = authorizationStructure;
    }

    public void ResolveInto(List<string> requestedAssetNames, QueryResolutionEvents resolutionEvents)
    {
      var assetsFromQuery = _authorizationStructure
        .RetrieveAssetsByOrganizationalUnitName(_name);
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