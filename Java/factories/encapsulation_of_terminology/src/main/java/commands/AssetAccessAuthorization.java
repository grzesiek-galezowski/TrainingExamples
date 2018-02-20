package commands;

import responseBuilders.AssetAuthorizationEvents;

public interface AssetAccessAuthorization {
    void verifyAccessTo(String assetName, String userName, AssetAuthorizationEvents assetAuthorizationEvents);
}
