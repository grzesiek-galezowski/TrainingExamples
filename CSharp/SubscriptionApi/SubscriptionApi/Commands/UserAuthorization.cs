using SubscriptionApi.ResponseBuilders;

namespace SubscriptionApi.Commands
{
  public interface UserAuthorization
  {
    void VerifyUserExistence(string userName, UserAuthorizationEvents userAuthorizationEvents);
  }
}