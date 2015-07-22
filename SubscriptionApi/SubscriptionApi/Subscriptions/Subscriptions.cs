using System.Collections.Generic;
using System.Linq;
using SubscriptionApi.ResponseBuilders;

namespace SubscriptionApi.Subscriptions
{
  public interface SubscriptionsModifyOperations
  {
    void AddNew(Subscription subscription);
    void Remove(string subscriptionId, SubscriptionStopEvents events);
  }

  public class Subscriptions : SubscriptionsModifyOperations
  {
    private readonly List<Subscription> _subscriptions = new List<Subscription>();
 
    public void AddNew(Subscription subscription)
    {
      subscription.ScheduleExpiry();
      _subscriptions.Add(subscription);
    }

    public void Remove(string subscriptionId, SubscriptionStopEvents events)
    {
      var subscription = _subscriptions.First(s => s.Has(subscriptionId));
      if (subscription != null)
      {
        subscription.ForceExpiry();
        _subscriptions.Remove(subscription);
      }
      else
      {
        events.NoSubscriptionToStopWithId(subscriptionId);
      }
    }
  }
}