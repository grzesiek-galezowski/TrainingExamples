package Commands;

import Dto.NewSubscriptionParametersDto;
import Dto.StoppedSubscriptionParametersDto;
import ResponseBuilders.SubscriptionStartResponseBuilder;
import ResponseBuilders.SubscriptionStopResponseBuilder;

public interface ICommandFactory {
    Command CreateFrom(NewSubscriptionParametersDto parameters, SubscriptionStartResponseBuilder responseBuilder);

    Command CreateFrom(StoppedSubscriptionParametersDto parameters, SubscriptionStopResponseBuilder responseBuilder);
}
