package subscriptions;

import dto.NewSubscriptionParametersDto;

public class Subscription {
    private final NewSubscriptionParametersDto parameters;

    public Subscription(NewSubscriptionParametersDto parameters) {
        this.parameters = parameters;
    }

    public boolean has(String subscriptionId) {
        return parameters.subscriptionId == subscriptionId;
    }

    public void forceExpiry() {
        throw new RuntimeException("not implemented");
    }

    public void scheduleExpiry() {
        throw new RuntimeException("not implemented");
    }
}
