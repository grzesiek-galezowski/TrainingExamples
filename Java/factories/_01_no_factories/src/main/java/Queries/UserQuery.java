package Queries;

import Authorization.AssetQueryResolution;

import java.util.List;

public class UserQuery implements AssetQuery
  {
    private final String _name;
    private AssetQueryResolution _authorizationStructure;

    public UserQuery(String name, AssetQueryResolution authorizationStructure)
    {
      _name = name;
      _authorizationStructure = authorizationStructure;
    }

    public void ResolveInto(List<String> requestedAssetNames, QueryResolutionEvents resolutionEvents)
    {
      List<String> assetsFromQuery = _authorizationStructure.RetrieveAssetsByUserName(_name);
      if (assetsFromQuery.isEmpty())
      {
        resolutionEvents.NoResolutionResultsFor(_name);
      }
      else
      {
        requestedAssetNames.addAll(assetsFromQuery);
      }
    }
  }