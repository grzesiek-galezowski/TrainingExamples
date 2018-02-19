package Authorization;

import java.util.List;

public interface AssetQueryResolution {
    List<String> RetrieveAssetsByHardwareName(String name);

    List<String> RetrieveAssetsByUserName(String name);

    List<String> RetrieveAssetsByOrganizationalUnitName(String name);
}
