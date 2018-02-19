package Queries;

import java.util.List;

public interface AssetQuery {
    void ResolveInto(List<String> requestedAssetNames, QueryResolutionEvents resolutionEvents);
}
