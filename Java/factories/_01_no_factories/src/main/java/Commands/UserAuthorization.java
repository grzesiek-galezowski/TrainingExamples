package Commands;

import ResponseBuilders.UserAuthorizationEvents;

public interface UserAuthorization {
    void VerifyUserExistence(String userName, UserAuthorizationEvents userAuthorizationEvents);
}
