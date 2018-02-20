package queries;

import java.util.List;

public interface AssetQuery {
    void resolveInto(List<String> requestedAssetNames, QueryResolutionEvents resolutionEvents);
}
