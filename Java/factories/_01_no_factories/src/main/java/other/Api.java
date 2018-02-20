package other;

import authorization.AssetQueryResolution;
import commands.AdapterFromSubscriptionCommandToCommand;
import commands.AssetAccessAuthorization;
import commands.Command;
import commands.ExceptionLoggedCommand;
import commands.ICommandFactory;
import commands.SubscriptionStartCommandFromApi;
import dto.NewSubscriptionParametersDto;
import dto.StartSubscriptionResponseDto;
import dto.StopSubscriptionResponseDto;
import dto.StoppedSubscriptionParametersDto;
import queries.AssetQuery;
import queries.HardwareQuery;
import queries.OrganizationalUnitQuery;
import queries.UserQuery;
import responseBuilders.SubscriptionStartResponseBuilder;
import responseBuilders.SubscriptionStopResponseBuilder;
import subscriptions.ISubscriptionFactory;
import subscriptions.SubscriptionsModifyOperations;

import java.util.List;

import static java.util.Arrays.asList;
import static java.util.stream.Collectors.toList;

public class Api {
    private final ICommandFactory commandFactory;
    private final ResponseBuilderFactory responseBuildersFactory;
    private AssetAccessAuthorization authorizationStructure;
    private ISubscriptionFactory subscriptionFactory;
    private final SubscriptionsModifyOperations subscriptions;
    private Log log;
    private AssetQueryResolution assetQueryResolution;

    public Api(
        ICommandFactory commandFactory,
        ResponseBuilderFactory responseBuildersFactory,
        Log log,
        AssetAccessAuthorization authorizationStructure,
        ISubscriptionFactory subscriptionFactory,
        SubscriptionsModifyOperations subscriptions,
        AssetQueryResolution assetQueryResolution) {
        this.log = log;
        this.commandFactory = commandFactory;
        this.responseBuildersFactory = responseBuildersFactory;
        this.authorizationStructure = authorizationStructure;
        this.subscriptionFactory = subscriptionFactory;
        this.subscriptions = subscriptions;
        this.assetQueryResolution = assetQueryResolution;
    }

    public StartSubscriptionResponseDto startSubscription(NewSubscriptionParametersDto parameters) {
        SubscriptionStartResponseBuilder responseBuilder
            = responseBuildersFactory.forStartSubscriptionResponse();
        //Command subscriptionStartCommand = commandFactory.createFrom(
        //    parameters, responseBuilder);

        //todo show how factories improve this code (uncomment code above
        //or refactor using bifunction<NewSubscriptionParametersDto, SubscriptionStartResponseBuilder, Command>;

        List<AssetQuery> assetQueries = asList(parameters.requests).stream().map(
            assetRequestDto -> {
                switch (assetRequestDto.kind) {
                    case User:
                        return new UserQuery(assetRequestDto.name, assetQueryResolution);
                    case OrganizationalUnit:
                        return new OrganizationalUnitQuery(assetRequestDto.name, assetQueryResolution);
                    case Hardware:
                        return new HardwareQuery(assetRequestDto.name, assetQueryResolution);
                    default:
                        throw new IllegalArgumentException();
                }
            }).collect(toList());
        Command subscriptionStartCommand = new ExceptionLoggedCommand(log,
            new AdapterFromSubscriptionCommandToCommand(
                new SubscriptionStartCommandFromApi(
                    parameters,
                    authorizationStructure,
                    responseBuilder,
                    subscriptionFactory,
                    subscriptions,
                    assetQueries)));

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
