using SubscriptionApi.Dto;
using SubscriptionApi.Exceptions;
using SubscriptionApi.ResponseBuilders;
using SubscriptionApi.Subscriptions;

namespace SubscriptionApi.Commands
{
  public interface SubscriptionStopCommand
  {
    StopSubscriptionResponseDto Response(); //bug is this used?
    void ValidateData();
    void Authorize();
    void Invoke();
  }

  public class SubscriptionStopCommandFromApi : SubscriptionStopCommand
  {
    private readonly StoppedSubscriptionParametersDto _parameters;
    private readonly SubscriptionStopResponseBuilder _responseBuilder;
    private readonly SubscriptionsModifyOperations _subscriptions;
    private readonly SubscriptionDataCorrectnessCriteria _correctnessCriteria;
    private readonly UserAuthorization _userAuthorization;

    public SubscriptionStopCommandFromApi(
      StoppedSubscriptionParametersDto parameters, 
      SubscriptionStopResponseBuilder responseBuilder, 
      SubscriptionsModifyOperations subscriptions, 
      SubscriptionDataCorrectnessCriteria correctnessCriteria, 
      UserAuthorization userAuthorization)
    {
      _parameters = parameters;
      _responseBuilder = responseBuilder;
      _subscriptions = subscriptions;
      _correctnessCriteria = correctnessCriteria;
      _userAuthorization = userAuthorization;
    }

    public StopSubscriptionResponseDto Response()
    {
      return _responseBuilder.Build();
    }

    public void ValidateData()
    {
      _correctnessCriteria.ValidateSessionId(_parameters.SubscriptionId, _responseBuilder);
      _correctnessCriteria.ValidateUserName(_parameters.UserName, _responseBuilder);
      _responseBuilder.AssertNoFatalErrors(new FatalErrorOccuredDuringValidationsException());
    }

    public void Authorize()
    {
      _userAuthorization.VerifyUserExistence(_parameters.UserName, _responseBuilder);
    }

    public void Invoke()
    {
      _subscriptions.Remove(_parameters.SubscriptionId, _responseBuilder);
    }
  }
}