package Commands;

import Authorization.AssetQueryResolution;
import Dto.AssetKind;
import Dto.AssetRequestDto;
import Queries.AssetQuery;
import Queries.HardwareQuery;
import Queries.OrganizationalUnitQuery;
import Queries.UserQuery;

import java.util.List;
import java.util.stream.StreamSupport;

import static java.util.stream.Collectors.toList;

public class AssetQueriesFactory implements IAssetQueriesFactory {
    private final AssetQueryResolution _authorizationStructure;

    public AssetQueriesFactory(AssetQueryResolution authorizationStructure) {
        _authorizationStructure = authorizationStructure;
    }

    public List<AssetQuery> CreateFrom(Iterable<AssetRequestDto> requests) {
        return StreamSupport.stream(requests.spliterator(), false).map(
            assetRequestDto -> AssetQueryFor(
                assetRequestDto.Name,
                assetRequestDto.Kind)).collect(toList());
    }

    private AssetQuery AssetQueryFor(String name, AssetKind assetKind) {
        switch (assetKind) {
            case User:
                return new UserQuery(name, _authorizationStructure);
            case OrganizationalUnit:
                return new OrganizationalUnitQuery(name, _authorizationStructure);
            case Hardware:
                return new HardwareQuery(name, _authorizationStructure);
            default:
                throw new IllegalArgumentException();
        }
    }
}
