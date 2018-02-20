package commands;

import dto.StopSubscriptionResponseDto;
import dto.StoppedSubscriptionParametersDto;
import exceptions.FatalErrorOccuredDuringValidationsException;
import responseBuilders.SubscriptionStopResponseBuilder;
import subscriptions.DataCorrectnessCriteria;
import subscriptions.SubscriptionsModifyOperations;

public class SubscriptionStopCommandFromApi
    implements SubscriptionCommand {
    private final StoppedSubscriptionParametersDto parameters;
    private final SubscriptionStopResponseBuilder responseBuilder;
    private final SubscriptionsModifyOperations subscriptions;
    private final DataCorrectnessCriteria correctnessCriteria;
    private final UserAuthorization userAuthorization;

    public SubscriptionStopCommandFromApi(
        StoppedSubscriptionParametersDto parameters,
        SubscriptionStopResponseBuilder responseBuilder,
        SubscriptionsModifyOperations subscriptions,
        DataCorrectnessCriteria correctnessCriteria,
        UserAuthorization userAuthorization) {
        this.parameters = parameters;
        this.responseBuilder = responseBuilder;
        this.subscriptions = subscriptions;
        this.correctnessCriteria = correctnessCriteria;
        this.userAuthorization = userAuthorization;
    }

    public StopSubscriptionResponseDto Response() {
        return responseBuilder.buildStop();
    }

    public void validateData() {
        correctnessCriteria.validateSessionId(parameters.subscriptionId, responseBuilder);
        correctnessCriteria.validateUserName(parameters.userName, responseBuilder);
        responseBuilder.assertNoFatalErrors(new FatalErrorOccuredDuringValidationsException());
    }

    public void authorize() {
        userAuthorization.verifyUserExistence(parameters.userName, responseBuilder);
    }

    public void invoke() {
        subscriptions.remove(parameters.subscriptionId, responseBuilder);
    }

    public void resolve() {

    }
}
