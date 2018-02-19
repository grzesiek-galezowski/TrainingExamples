package Subscriptions;

import Dto.NewSubscriptionParametersDto;

public interface ISubscriptionFactory {
    Subscription CreateFrom(NewSubscriptionParametersDto parameters);
}
