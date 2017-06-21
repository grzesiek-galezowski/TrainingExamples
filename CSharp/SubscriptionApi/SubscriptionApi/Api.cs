using System;
using SubscriptionApi.Commands;
using SubscriptionApi.Dto;

namespace SubscriptionApi
{
  public class Api
  {
    private readonly ICommandFactory _commandFactory;
    private readonly ResponseBuilderFactory _responseBuildersFactory;

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
      var responseBuilder = _responseBuildersFactory.ForStartSubscriptionResponse();
      var subscriptionStartCommand = _commandFactory.CreateFrom(parameters, responseBuilder);

      subscriptionStartCommand.Invoke();
      
      return responseBuilder.Build();
    }

    public StopSubscriptionResponseDto StopSubscription(StoppedSubscriptionParametersDto parameters)
    {
      var responseBuilder = _responseBuildersFactory.ForStopSubscriptionResponse();
      var subscriptionStopCommand = _commandFactory.CreateFrom(parameters, responseBuilder);

      subscriptionStopCommand.Invoke();

      return responseBuilder.Build();
    }
  }
}