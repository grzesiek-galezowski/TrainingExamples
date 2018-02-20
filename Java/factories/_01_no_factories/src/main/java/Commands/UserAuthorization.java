package commands;

import responseBuilders.UserAuthorizationEvents;

public interface UserAuthorization {
    void verifyUserExistence(String userName, UserAuthorizationEvents userAuthorizationEvents);
}
