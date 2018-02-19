package Subscriptions;

import ResponseBuilders.SubscriptionStopEvents;

import java.util.ArrayList;
import java.util.List;

public class Subscriptions implements Subscriptions.SubscriptionsModifyOperations {
    private final List<Subscriptions.Subscription> _subscriptions = new ArrayList<Subscriptions.Subscription>();

    public void AddNew(Subscriptions.Subscription subscription) {
        subscription.ScheduleExpiry();
        _subscriptions.add(subscription);
    }

    public void Remove(String subscriptionId, SubscriptionStopEvents events) {
        Subscription subscription = _subscriptions.stream().filter(s -> s.Has(subscriptionId)).findFirst().get();
        if (subscription != null) {
            subscription.ForceExpiry();
            _subscriptions.remove(subscription);
        } else {
            events.NoSubscriptionToStopWithId(subscriptionId);
        }
    }
}
