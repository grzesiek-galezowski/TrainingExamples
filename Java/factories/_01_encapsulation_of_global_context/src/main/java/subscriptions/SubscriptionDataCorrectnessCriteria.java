package subscriptions;

import responseBuilders.SubscriptionValidationResults;

public class SubscriptionDataCorrectnessCriteria implements DataCorrectnessCriteria {
    @Override
    public void validateSessionId(String subscriptionId, SubscriptionValidationResults results) {
        if ("".equals(subscriptionId) || subscriptionId == null) {
            results.notValid("Subscription ID");
        }
    }

    @Override
    public void validateUserName(String userName, SubscriptionValidationResults results) {
        if ("".equals(userName) || userName == null) {
            results.notValid("user name");
        }
    }
}
