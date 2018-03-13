package queries;

import authorization.AssetQueryResolution;

import java.util.List;

public class OrganizationalUnitQuery implements AssetQuery {
    private final String name;
    private final AssetQueryResolution authorizationStructure;

    public OrganizationalUnitQuery(String name, AssetQueryResolution authorizationStructure) {
        this.name = name;
        this.authorizationStructure = authorizationStructure;
    }

    public void resolveInto(List<String> requestedAssetNames, QueryResolutionEvents resolutionEvents) {
        List<String> assetsFromQuery = authorizationStructure
            .retrieveAssetsByOrganizationalUnitName(name);
        if (assetsFromQuery.isEmpty()) {
            resolutionEvents.noResolutionResultsFor(name);
        } else {
            requestedAssetNames.addAll(assetsFromQuery);
        }

    }
}
