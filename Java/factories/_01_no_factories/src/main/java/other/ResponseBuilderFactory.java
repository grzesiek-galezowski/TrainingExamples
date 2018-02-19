package other;

import ResponseBuilders.SubscriptionStartResponseBuilder;
import ResponseBuilders.SubscriptionStopResponseBuilder;

public interface ResponseBuilderFactory {
    SubscriptionStartResponseBuilder ForStartSubscriptionResponse();

    SubscriptionStopResponseBuilder ForStopSubscriptionResponse();
}

public class DefaultResponseBuilderFactory implements ResponseBuilderFactory {
    public SubscriptionStartResponseBuilder ForStartSubscriptionResponse() {
        return new SubscriptionResponseBuilder();
    }

    public SubscriptionStopResponseBuilder ForStopSubscriptionResponse() {
        return new SubscriptionResponseBuilder();
    }
}
