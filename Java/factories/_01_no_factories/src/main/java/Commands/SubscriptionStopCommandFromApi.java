package Commands;

import Dto.StopSubscriptionResponseDto;
import Dto.StoppedSubscriptionParametersDto;
import Exceptions.FatalErrorOccuredDuringValidationsException;
import ResponseBuilders.SubscriptionStopResponseBuilder;
import Subscriptions.SubscriptionDataCorrectnessCriteria;
import Subscriptions.SubscriptionsModifyOperations;

public class SubscriptionStopCommandFromApi
    implements SubscriptionCommand {
    private final StoppedSubscriptionParametersDto _parameters;
    private final SubscriptionStopResponseBuilder _responseBuilder;
    private final SubscriptionsModifyOperations _subscriptions;
    private final SubscriptionDataCorrectnessCriteria _correctnessCriteria;
    private final UserAuthorization _userAuthorization;

    public SubscriptionStopCommandFromApi(
        StoppedSubscriptionParametersDto parameters,
        SubscriptionStopResponseBuilder responseBuilder,
        SubscriptionsModifyOperations subscriptions,
        SubscriptionDataCorrectnessCriteria correctnessCriteria,
        UserAuthorization userAuthorization) {
        _parameters = parameters;
        _responseBuilder = responseBuilder;
        _subscriptions = subscriptions;
        _correctnessCriteria = correctnessCriteria;
        _userAuthorization = userAuthorization;
    }

    public StopSubscriptionResponseDto Response() {
        return _responseBuilder.BuildStop();
    }

    public void ValidateData() {
        _correctnessCriteria.ValidateSessionId(_parameters.SubscriptionId, _responseBuilder);
        _correctnessCriteria.ValidateUserName(_parameters.UserName, _responseBuilder);
        _responseBuilder.AssertNoFatalErrors(new FatalErrorOccuredDuringValidationsException());
    }

    public void Authorize() {
        _userAuthorization.VerifyUserExistence(_parameters.UserName, _responseBuilder);
    }

    public void Invoke() {
        _subscriptions.Remove(_parameters.SubscriptionId, _responseBuilder);
    }

    public void Resolve() {

    }
}
