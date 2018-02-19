package Subscriptions;

import ResponseBuilders.SubscriptionStopEvents;

public interface SubscriptionsModifyOperations {
    void AddNew(Subscription subscription);

    void Remove(String subscriptionId, SubscriptionStopEvents events);
}
