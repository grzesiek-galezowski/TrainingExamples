package com.github.TrainingExamples.grzesiek_galezowski.commands;

import dto.AssetRequestDto;
import queries.AssetQuery;

import java.util.List;

public interface IAssetQueriesFactory {
    List<AssetQuery> createFrom(Iterable<AssetRequestDto> requests);
}
