package Subscriptions;

import ResponseBuilders.SubscriptionValidationResults;

public class SubscriptionDataCorrectnessCriteria {
    public void ValidateSessionId(String subscriptionId, SubscriptionValidationResults results) {
        if ("".equals(subscriptionId) || subscriptionId == null) {
            results.NotValid("Subscription ID");
        }
    }

    public void ValidateUserName(String userName, SubscriptionValidationResults results) {
        if ("".equals(userName) || userName == null) {
            results.NotValid("user name");
        }
    }
}
