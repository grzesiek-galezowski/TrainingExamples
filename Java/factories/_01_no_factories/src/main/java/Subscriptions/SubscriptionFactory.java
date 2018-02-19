package Subscriptions;

import Dto.NewSubscriptionParametersDto;

public class SubscriptionFactory implements ISubscriptionFactory {
    public Subscription CreateFrom(NewSubscriptionParametersDto parameters) {
        return new Subscription(parameters);
    }
}
