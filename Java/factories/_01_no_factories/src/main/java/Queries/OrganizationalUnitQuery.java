package Queries;

import Authorization.AssetQueryResolution;

import java.util.List;

public class OrganizationalUnitQuery implements AssetQuery {
    private final String _name;
    private final AssetQueryResolution _authorizationStructure;

    public OrganizationalUnitQuery(String name, AssetQueryResolution authorizationStructure) {
        _name = name;
        _authorizationStructure = authorizationStructure;
    }

    public void ResolveInto(List<String> requestedAssetNames, QueryResolutionEvents resolutionEvents) {
        List<String> assetsFromQuery = _authorizationStructure
            .RetrieveAssetsByOrganizationalUnitName(_name);
        if (assetsFromQuery.isEmpty()) {
            resolutionEvents.NoResolutionResultsFor(_name);
        } else {
            requestedAssetNames.addAll(assetsFromQuery);
        }

    }
}
