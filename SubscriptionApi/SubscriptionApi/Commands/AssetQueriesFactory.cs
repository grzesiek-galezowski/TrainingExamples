using System;
using System.Collections.Generic;
using System.Linq;
using SubscriptionApi.Authorization;
using SubscriptionApi.Dto;
using SubscriptionApi.Queries;

namespace SubscriptionApi.Commands
{
  public interface IAssetQueriesFactory
  {
    List<AssetQuery> CreateFrom(IEnumerable<AssetRequestDto> requests);
  }

  public class AssetQueriesFactory : IAssetQueriesFactory
  {
    private readonly AssetQueryResolution _authorizationStructure;

    public AssetQueriesFactory(AssetQueryResolution authorizationStructure)
    {
      _authorizationStructure = authorizationStructure;
    }

    public List<AssetQuery> CreateFrom(IEnumerable<AssetRequestDto> requests)
    {
      return requests.Select(
        assetRequestDto => AssetQueryFor(
          assetRequestDto.Name, 
          assetRequestDto.Kind)).ToList();
    }

    private AssetQuery AssetQueryFor(string name, AssetKind assetKind)
    {
      switch (assetKind)
      {
        case AssetKind.User:
          return new UserQuery(name, _authorizationStructure);
        case AssetKind.OrganizationalUnit:
          return new OrganizationalUnitQuery(name, _authorizationStructure);
        case AssetKind.Hardware:
          return new HardwareQuery(name, _authorizationStructure);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}