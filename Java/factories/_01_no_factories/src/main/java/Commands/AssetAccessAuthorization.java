package Commands;

import ResponseBuilders.AssetAuthorizationEvents;

public interface AssetAccessAuthorization {
    void VerifyAccessTo(String assetName, String userName, AssetAuthorizationEvents assetAuthorizationEvents);
}
