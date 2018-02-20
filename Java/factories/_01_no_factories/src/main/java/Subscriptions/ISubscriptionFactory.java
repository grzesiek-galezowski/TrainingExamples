package subscriptions;

import dto.NewSubscriptionParametersDto;

public interface ISubscriptionFactory {
    Subscription createFrom(NewSubscriptionParametersDto parameters);
}
