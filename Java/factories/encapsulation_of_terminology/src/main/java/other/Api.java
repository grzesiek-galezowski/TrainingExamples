package other;

import commands.Command;
import commands.ICommandFactory;
import dto.NewSubscriptionParametersDto;
import dto.StartSubscriptionResponseDto;
import dto.StopSubscriptionResponseDto;
import dto.StoppedSubscriptionParametersDto;
import responseBuilders.SubscriptionStartResponseBuilder;
import responseBuilders.SubscriptionStopResponseBuilder;

public class Api {
    private final ICommandFactory commandFactory;
    private final ResponseBuilderFactory responseBuildersFactory;

    public Api(
        ICommandFactory commandFactory,
        ResponseBuilderFactory responseBuildersFactory,
        Log log) {
        this.commandFactory = commandFactory;
        this.responseBuildersFactory = responseBuildersFactory;
    }

    public StartSubscriptionResponseDto startSubscription(NewSubscriptionParametersDto parameters) {
        SubscriptionStartResponseBuilder responseBuilder = responseBuildersFactory.forStartSubscriptionResponse();
        Command subscriptionStartCommand = commandFactory.createFrom(parameters, responseBuilder);

        subscriptionStartCommand.invoke();

        return responseBuilder.buildStart();
    }

    public StopSubscriptionResponseDto stopSubscription(StoppedSubscriptionParametersDto parameters) {
        SubscriptionStopResponseBuilder responseBuilder = responseBuildersFactory.forStopSubscriptionResponse();
        Command subscriptionStopCommand = commandFactory.createFrom(parameters, responseBuilder);

        subscriptionStopCommand.invoke();

        return responseBuilder.buildStop();
    }
}
