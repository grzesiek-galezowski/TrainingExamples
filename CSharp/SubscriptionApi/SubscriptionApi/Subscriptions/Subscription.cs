using SubscriptionApi.Dto;

namespace SubscriptionApi.Subscriptions
{
  public class Subscription
  {
    private readonly NewSubscriptionParametersDto _parameters;

    public Subscription(NewSubscriptionParametersDto parameters)
    {
      _parameters = parameters;
    }

    public bool Has(string subscriptionId)
    {
      return _parameters.SubscriptionId == subscriptionId;
    }

    public void ForceExpiry()
    {
      throw new System.NotImplementedException();
    }

    public void ScheduleExpiry()
    {
      throw new System.NotImplementedException();
    }
  }
}