using SubscriptionApi.ResponseBuilders;

namespace SubscriptionApi.Commands
{
  public interface AssetAccessAuthorization
  {
    void VerifyAccessTo(string assetName, string userName, AssetAuthorizationEvents assetAuthorizationEvents);
  }
}