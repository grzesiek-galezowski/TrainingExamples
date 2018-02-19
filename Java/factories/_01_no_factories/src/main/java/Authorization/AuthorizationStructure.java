package Authorization;

import Commands.AssetAccessAuthorization;
import Commands.UserAuthorization;
import ResponseBuilders.AssetAuthorizationEvents;
import ResponseBuilders.UserAuthorizationEvents;

import java.util.List;

public class AuthorizationStructure implements
    AssetAccessAuthorization,
    UserAuthorization,
    AssetQueryResolution {
    public void VerifyAccessTo(String assetName, String userName, AssetAuthorizationEvents events) {
        events.NotAuthorizedForAsset(assetName, userName);
    }

    public void VerifyUserExistence(String userName, UserAuthorizationEvents events) {
        events.UserNotAuthorized(userName);
    }

    public List<String> RetrieveAssetsByHardwareName(String name) {
        throw new RuntimeException("not implemented");

    }

    public List<String> RetrieveAssetsByUserName(String name) {
        throw new RuntimeException("not implemented");
    }

    public List<String> RetrieveAssetsByOrganizationalUnitName(String name) {
        throw new RuntimeException("not implemented");
    }
}
