package Subscriptions;

import Dto.NewSubscriptionParametersDto;

public class Subscription {
    private final NewSubscriptionParametersDto _parameters;

    public Subscription(NewSubscriptionParametersDto parameters) {
        _parameters = parameters;
    }

    public boolean Has(String subscriptionId) {
        return _parameters.SubscriptionId == subscriptionId;
    }

    public void ForceExpiry() {
        throw new RuntimeException("not implemented");
    }

    public void ScheduleExpiry() {
        throw new RuntimeException("not implemented");
    }
}
