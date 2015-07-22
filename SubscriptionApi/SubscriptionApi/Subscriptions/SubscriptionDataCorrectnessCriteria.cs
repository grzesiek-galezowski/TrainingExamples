using SubscriptionApi.ResponseBuilders;

namespace SubscriptionApi.Subscriptions
{
  public class SubscriptionDataCorrectnessCriteria
  {
    public void ValidateSessionId(string subscriptionId, SubscriptionValidationResults results)
    {
      if (string.IsNullOrEmpty(subscriptionId))
      {
        results.NotValid("Subscription ID");
      }
    }

    public void ValidateUserName(string userName, SubscriptionValidationResults results)
    {
      if (string.IsNullOrEmpty(userName))
      {
        results.NotValid("user name");
      }
    }
  }
}