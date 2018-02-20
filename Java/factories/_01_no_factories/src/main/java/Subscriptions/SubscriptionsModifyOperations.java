package subscriptions;

import responseBuilders.SubscriptionStopEvents;

public interface SubscriptionsModifyOperations {
    void addNew(Subscription subscription);

    void remove(String subscriptionId, SubscriptionStopEvents events);
}
