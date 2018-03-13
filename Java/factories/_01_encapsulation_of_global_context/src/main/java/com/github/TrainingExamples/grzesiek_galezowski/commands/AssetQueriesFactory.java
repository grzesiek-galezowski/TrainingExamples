package com.github.TrainingExamples.grzesiek_galezowski.commands;

import authorization.AssetQueryResolution;
import dto.AssetKind;
import dto.AssetRequestDto;
import queries.AssetQuery;
import queries.HardwareQuery;
import queries.OrganizationalUnitQuery;
import queries.UserQuery;

import java.util.List;
import java.util.stream.StreamSupport;

import static java.util.stream.Collectors.toList;

public class AssetQueriesFactory implements IAssetQueriesFactory {
    private final AssetQueryResolution authorizationStructure;

    public AssetQueriesFactory(AssetQueryResolution authorizationStructure) {
        this.authorizationStructure = authorizationStructure;
    }

    public List<AssetQuery> createFrom(Iterable<AssetRequestDto> requests) {
        return StreamSupport.stream(requests.spliterator(), false).map(
            assetRequestDto -> assetQueryFor(
                assetRequestDto.name,
                assetRequestDto.kind)).collect(toList());
    }

    private AssetQuery assetQueryFor(String name, AssetKind assetKind) {
        switch (assetKind) {
            case User:
                return new UserQuery(name, authorizationStructure);
            case OrganizationalUnit:
                return new OrganizationalUnitQuery(name, authorizationStructure);
            case Hardware:
                return new HardwareQuery(name, authorizationStructure);
            default:
                throw new IllegalArgumentException();
        }
    }
}
