package queries;

import authorization.AssetQueryResolution;

import java.util.List;

public class UserQuery implements AssetQuery {
    private final String name;
    private AssetQueryResolution authorizationStructure;

    public UserQuery(String name, AssetQueryResolution authorizationStructure) {
        this.name = name;
        this.authorizationStructure = authorizationStructure;
    }

    public void resolveInto(List<String> requestedAssetNames, QueryResolutionEvents resolutionEvents) {
        List<String> assetsFromQuery = authorizationStructure.retrieveAssetsByUserName(name);
        if (assetsFromQuery.isEmpty()) {
            resolutionEvents.noResolutionResultsFor(name);
        } else {
            requestedAssetNames.addAll(assetsFromQuery);
        }
    }
}