package other;

import Dto.NewSubscriptionParametersDto;
import Dto.StartSubscriptionResponseDto;
import Dto.StopSubscriptionResponseDto;
import Dto.StoppedSubscriptionParametersDto;
import ResponseBuilders.SubscriptionStartResponseBuilder;
import ResponseBuilders.SubscriptionStopResponseBuilder;

public class Api
  {
    private final ICommandFactory _commandFactory;
    private final ResponseBuilderFactory _responseBuildersFactory;

    public Api(
      ICommandFactory commandFactory, 
      ResponseBuilderFactory responseBuildersFactory,
      Log log)
    {
      _commandFactory = commandFactory;
      _responseBuildersFactory = responseBuildersFactory;
    }

    public StartSubscriptionResponseDto StartSubscription(NewSubscriptionParametersDto parameters)
    {
      SubscriptionStartResponseBuilder responseBuilder = _responseBuildersFactory.ForStartSubscriptionResponse();
      Command subscriptionStartCommand = _commandFactory.CreateFrom(parameters, responseBuilder);

      subscriptionStartCommand.Invoke();
      
      return responseBuilder.BuildStart();
    }

    public StopSubscriptionResponseDto StopSubscription(StoppedSubscriptionParametersDto parameters)
    {
      SubscriptionStopResponseBuilder responseBuilder = _responseBuildersFactory.ForStopSubscriptionResponse();
      Command subscriptionStopCommand = _commandFactory.CreateFrom(parameters, responseBuilder);

      subscriptionStopCommand.Invoke();

      return responseBuilder.BuildStop();
    }
  }
