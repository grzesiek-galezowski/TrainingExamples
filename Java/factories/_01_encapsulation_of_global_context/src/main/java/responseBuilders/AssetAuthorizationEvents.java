package responseBuilders;

public interface AssetAuthorizationEvents {
    void notAuthorizedForAsset(String assetName, String userName);
}
