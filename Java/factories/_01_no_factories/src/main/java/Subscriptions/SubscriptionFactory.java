package subscriptions;

import dto.NewSubscriptionParametersDto;

public class SubscriptionFactory implements ISubscriptionFactory {
    @Override
    public Subscription createFrom(NewSubscriptionParametersDto parameters) {
        return new Subscription(parameters);
    }
}
