package subscriptions;

import responseBuilders.SubscriptionValidationResults;

public interface DataCorrectnessCriteria {
    void validateSessionId(String subscriptionId, SubscriptionValidationResults results);

    void validateUserName(String userName, SubscriptionValidationResults results);
}
