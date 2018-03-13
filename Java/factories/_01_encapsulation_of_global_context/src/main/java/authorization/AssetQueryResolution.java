package authorization;

import java.util.List;

public interface AssetQueryResolution {
    List<String> retrieveAssetsByHardwareName(String name);

    List<String> retrieveAssetsByUserName(String name);

    List<String> retrieveAssetsByOrganizationalUnitName(String name);
}
