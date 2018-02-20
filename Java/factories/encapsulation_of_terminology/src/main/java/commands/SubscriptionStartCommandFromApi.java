package commands;

import dto.NewSubscriptionParametersDto;
import dto.StartSubscriptionResponseDto;
import exceptions.FatalErrorOccuredDuringAuthorizationException;
import exceptions.FatalErrorOccuredDuringQueryResolutionException;
import queries.AssetQuery;
import responseBuilders.SubscriptionStartResponseBuilder;
import subscriptions.ISubscriptionFactory;
import subscriptions.Subscription;
import subscriptions.SubscriptionsModifyOperations;

import java.util.ArrayList;
import java.util.List;

public class SubscriptionStartCommandFromApi implements SubscriptionCommand {
    private final NewSubscriptionParametersDto parametersDto;
    private final List<String> _requestedAssetNames = new ArrayList<>();
    private final AssetAccessAuthorization authorizationStructure;
    private final SubscriptionStartResponseBuilder responseBuilder;
    private final ISubscriptionFactory subscriptionFactory;
    private final List<AssetQuery> assetQueries;
    private final SubscriptionsModifyOperations subscriptions;


    public SubscriptionStartCommandFromApi(
        NewSubscriptionParametersDto parameters,
        AssetAccessAuthorization authorizationStructure,
        SubscriptionStartResponseBuilder responseBuilder,
        ISubscriptionFactory subscriptionFactory,
        SubscriptionsModifyOperations subscriptions,
        List<AssetQuery> assetQueries) {

        this.parametersDto = parameters;
        this.authorizationStructure = authorizationStructure;
        this.responseBuilder = responseBuilder;
        this.subscriptionFactory = subscriptionFactory;
        this.subscriptions = subscriptions;
        this.assetQueries = assetQueries;
    }

    public void validateData() {
        //TODO user name
        //TODO duration
        //TODO validate each query

        throw new RuntimeException("not implemented");
    }

    public void resolve() {
        for (AssetQuery query : assetQueries) {
            query.resolveInto(_requestedAssetNames, responseBuilder);
        }

        responseBuilder.assertNoFatalErrors(
            new FatalErrorOccuredDuringQueryResolutionException());
    }

    public void authorize() {
        for (String assetName : _requestedAssetNames) {
            authorizationStructure.verifyAccessTo(
                assetName,
                parametersDto.userName,
                responseBuilder);
        }

        responseBuilder.assertNoFatalErrors(
            new FatalErrorOccuredDuringAuthorizationException());
    }

    public void invoke() {
        Subscription subscription = subscriptionFactory.createFrom(parametersDto);
        subscriptions.addNew(subscription);
    }


    public StartSubscriptionResponseDto response() {
        return responseBuilder.buildStart();
    }
}
