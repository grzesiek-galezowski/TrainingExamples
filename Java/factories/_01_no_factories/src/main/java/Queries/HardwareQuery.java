package Queries;

import Authorization.AssetQueryResolution;

import java.util.List;

public class HardwareQuery implements AssetQuery {
    private final String _name;
    private final AssetQueryResolution _authorizationStructure;

    public HardwareQuery(String name, AssetQueryResolution authorizationStructure) {
        _name = name;
        _authorizationStructure = authorizationStructure;
    }

    public void ResolveInto(List<String> requestedAssetNames, QueryResolutionEvents resolutionEvents) {
        List<String> assetsFromQuery = _authorizationStructure.RetrieveAssetsByHardwareName(_name);
        if (assetsFromQuery.isEmpty()) {
            resolutionEvents.NoResolutionResultsFor(_name);
        } else {
            requestedAssetNames.addAll(assetsFromQuery);
        }
    }
}
