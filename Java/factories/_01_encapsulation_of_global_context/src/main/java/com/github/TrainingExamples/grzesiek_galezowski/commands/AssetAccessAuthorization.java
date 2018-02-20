package com.github.TrainingExamples.grzesiek_galezowski.commands;

import responseBuilders.AssetAuthorizationEvents;

public interface AssetAccessAuthorization {
    void verifyAccessTo(String assetName, String userName, AssetAuthorizationEvents assetAuthorizationEvents);
}
