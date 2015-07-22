using System.Collections.Generic;

namespace SubscriptionApi.Authorization
{
  public interface AssetQueryResolution
  {
    List<string> RetrieveAssetsByHardwareName(string name);
    List<string> RetrieveAssetsByUserName(string name);
    List<string> RetrieveAssetsByOrganizationalUnitName(string name);
  }
}