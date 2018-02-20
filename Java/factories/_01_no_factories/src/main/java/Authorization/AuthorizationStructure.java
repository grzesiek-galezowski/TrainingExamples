package authorization;

import commands.AssetAccessAuthorization;
import commands.UserAuthorization;
import responseBuilders.AssetAuthorizationEvents;
import responseBuilders.UserAuthorizationEvents;

import java.util.List;

public class AuthorizationStructure implements
    AssetAccessAuthorization,
    UserAuthorization,
    AssetQueryResolution {
    public void verifyAccessTo(String assetName, String userName, AssetAuthorizationEvents events) {
        events.notAuthorizedForAsset(assetName, userName);
    }

    public void verifyUserExistence(String userName, UserAuthorizationEvents events) {
        events.userNotAuthorized(userName);
    }

    public List<String> retrieveAssetsByHardwareName(String name) {
        throw new RuntimeException("not implemented");

    }

    public List<String> retrieveAssetsByUserName(String name) {
        throw new RuntimeException("not implemented");
    }

    public List<String> retrieveAssetsByOrganizationalUnitName(String name) {
        throw new RuntimeException("not implemented");
    }
}
