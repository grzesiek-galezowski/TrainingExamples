namespace SubscriptionApi.ResponseBuilders
{
  public interface AssetAuthorizationEvents
  {
    void NotAuthorizedForAsset(string assetName, string userName);
  }
}