package subscriptions;

import responseBuilders.SubscriptionStopEvents;

import java.util.ArrayList;
import java.util.List;

public class Subscriptions implements SubscriptionsModifyOperations {
    private final List<Subscription> _subscriptions = new ArrayList<>();

    @Override
    public void addNew(Subscription subscription) {
        subscription.scheduleExpiry();
        _subscriptions.add(subscription);
    }

    @Override
    public void remove(String subscriptionId, SubscriptionStopEvents events) {
        Subscription subscription = _subscriptions.stream().filter(s -> s.has(subscriptionId)).findFirst().get();
        if (subscription != null) {
            subscription.forceExpiry();
            _subscriptions.remove(subscription);
        } else {
            events.noSubscriptionToStopWithId(subscriptionId);
        }
    }
}
