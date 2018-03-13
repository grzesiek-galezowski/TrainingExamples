package other;

import responseBuilders.SubscriptionStartResponseBuilder;
import responseBuilders.SubscriptionStopResponseBuilder;

public interface ResponseBuilderFactory {
    SubscriptionStartResponseBuilder forStartSubscriptionResponse();

    SubscriptionStopResponseBuilder forStopSubscriptionResponse();
}

