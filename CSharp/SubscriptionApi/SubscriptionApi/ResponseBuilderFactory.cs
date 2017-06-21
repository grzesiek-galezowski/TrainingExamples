using SubscriptionApi.ResponseBuilders;

namespace SubscriptionApi
{
  //bug move to specific namespace
  public interface ResponseBuilderFactory
  {
    SubscriptionStartResponseBuilder ForStartSubscriptionResponse();
    SubscriptionStopResponseBuilder ForStopSubscriptionResponse();
  }

  public class DefaultResponseBuilderFactory : ResponseBuilderFactory
  {
    public SubscriptionStartResponseBuilder ForStartSubscriptionResponse()
    {
      return new SubscriptionResponseBuilder();
    }

    public SubscriptionStopResponseBuilder ForStopSubscriptionResponse()
    {
      return new SubscriptionResponseBuilder();
    }
  }
}