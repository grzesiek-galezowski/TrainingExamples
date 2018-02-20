package other;

import responseBuilders.SubscriptionResponseBuilder;
import responseBuilders.SubscriptionStartResponseBuilder;
import responseBuilders.SubscriptionStopResponseBuilder;

public class DefaultResponseBuilderFactory implements ResponseBuilderFactory {
    @Override
    public SubscriptionStartResponseBuilder forStartSubscriptionResponse() {
        return new SubscriptionResponseBuilder();
    }

    @Override
    public SubscriptionStopResponseBuilder forStopSubscriptionResponse() {
        return new SubscriptionResponseBuilder();
    }
}
