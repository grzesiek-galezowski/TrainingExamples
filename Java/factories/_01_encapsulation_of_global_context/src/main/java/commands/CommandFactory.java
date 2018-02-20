package commands;

import authorization.AuthorizationStructure;
import dto.NewSubscriptionParametersDto;
import dto.StoppedSubscriptionParametersDto;
import queries.AssetQuery;
import responseBuilders.SubscriptionStartResponseBuilder;
import responseBuilders.SubscriptionStopResponseBuilder;
import subscriptions.DataCorrectnessCriteria;
import subscriptions.ISubscriptionFactory;
import subscriptions.SubscriptionsModifyOperations;
import other.Log;

import java.util.List;

import static java.util.Arrays.asList;

public class CommandFactory implements ICommandFactory {
    private final SubscriptionsModifyOperations subscriptions;
    private final AuthorizationStructure authorizationStructure;
    private final ISubscriptionFactory subscriptionFactory;
    private final IAssetQueriesFactory assetQueriesFactory;
    private final Log log;
    private final DataCorrectnessCriteria dataCorrectnessCriteria;

    public CommandFactory(
        SubscriptionsModifyOperations subscriptions,
        AuthorizationStructure authorizationStructure,
        ISubscriptionFactory subscriptionFactory,
        DataCorrectnessCriteria dataCorrectnessCriteria,
        IAssetQueriesFactory assetQueriesFactory,
        Log log) {
        this.subscriptions = subscriptions;
        this.authorizationStructure = authorizationStructure;
        this.subscriptionFactory = subscriptionFactory;
        this.dataCorrectnessCriteria = dataCorrectnessCriteria;
        this.assetQueriesFactory = assetQueriesFactory;
        this.log = log;
    }

    public Command createFrom(NewSubscriptionParametersDto parameters, SubscriptionStartResponseBuilder responseBuilder) {
        List<AssetQuery> assetQueries = assetQueriesFactory
            .createFrom(asList(parameters.requests));
        return
            new ExceptionLoggedCommand(log,
                new AdapterFromSubscriptionCommandToCommand(
                    new SubscriptionStartCommandFromApi(
                        parameters,
                        authorizationStructure,
                        responseBuilder,
                        subscriptionFactory,
                        subscriptions,
                        assetQueries)));
    }

    public Command createFrom(StoppedSubscriptionParametersDto parameters, SubscriptionStopResponseBuilder responseBuilder) {
        return
            new ExceptionLoggedCommand(log,
                new AdapterFromSubscriptionCommandToCommand(
                    new SubscriptionStopCommandFromApi(
                        parameters,
                        responseBuilder,
                        subscriptions,
                        dataCorrectnessCriteria,
                        authorizationStructure)));
    }
}
