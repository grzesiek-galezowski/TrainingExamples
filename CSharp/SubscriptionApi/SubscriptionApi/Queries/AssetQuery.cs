using System.Collections.Generic;

namespace SubscriptionApi.Queries
{
  public interface AssetQuery
  {
    void ResolveInto(List<string> requestedAssetNames, QueryResolutionEvents resolutionEvents);
  }
}