package com.github.TrainingExamples.grzesiek_galezowski.commands;

import responseBuilders.UserAuthorizationEvents;

public interface UserAuthorization {
    void verifyUserExistence(String userName, UserAuthorizationEvents userAuthorizationEvents);
}
