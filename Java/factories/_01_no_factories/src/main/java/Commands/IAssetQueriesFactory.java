package Commands;

import Dto.AssetRequestDto;
import Queries.AssetQuery;

import java.util.List;

public interface IAssetQueriesFactory {
    List<AssetQuery> CreateFrom(Iterable<AssetRequestDto> requests);
}
