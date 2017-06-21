using System.Collections.Generic;
using SubscriptionApi.Commands;
using SubscriptionApi.ResponseBuilders;

namespace SubscriptionApi.Authorization
{
  public class AuthorizationStructure : 
    AssetAccessAuthorization,
    UserAuthorization, 
    AssetQueryResolution
  {
    public void VerifyAccessTo(string assetName, string userName, AssetAuthorizationEvents events)
    {
      events.NotAuthorizedForAsset(assetName, userName);
    }

    public void VerifyUserExistence(string userName, UserAuthorizationEvents events)
    {
      events.UserNotAuthorized(userName);
    }

    public List<string> RetrieveAssetsByHardwareName(string name)
    {
      throw new System.NotImplementedException();
    }

    public List<string> RetrieveAssetsByUserName(string name)
    {
      throw new System.NotImplementedException();
    }

    public List<string> RetrieveAssetsByOrganizationalUnitName(string name)
    {
      throw new System.NotImplementedException();
    }
  }
}