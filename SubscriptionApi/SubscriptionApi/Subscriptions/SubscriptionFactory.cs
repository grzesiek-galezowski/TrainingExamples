using SubscriptionApi.Dto;

namespace SubscriptionApi.Subscriptions
{
  public interface ISubscriptionFactory
  {
    Subscription CreateFrom(NewSubscriptionParametersDto parameters);
  }

  public class SubscriptionFactory : ISubscriptionFactory
  {
    public Subscription CreateFrom(NewSubscriptionParametersDto parameters)
    {
      return new Subscription(parameters);
    }
  }
}