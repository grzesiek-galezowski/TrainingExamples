using System;
using SubscriptionApi.Commands;
using SubscriptionApi.Dto;

namespace SubscriptionApi
{
  public class Api
  {
    private readonly ICommandFactory _commandFactory;
    private readonly CommandProcessing _commandProcessing;
    private readonly Log _log;

    public Api(
      ICommandFactory commandFactory, 
      CommandProcessing commandProcessing,
      Log log)
    {
      _commandFactory = commandFactory;
      _commandProcessing = commandProcessing;
      _log = log;
    }

    public StartSubscriptionResponseDto StartSubscription(NewSubscriptionParametersDto parameters)
    {
      var subscriptionStartCommand = _commandFactory.CreateFrom(parameters);
      
      try
      {
        _commandProcessing.ApplyTo(subscriptionStartCommand);
      }
      catch (Exception e)
      {
        //bug log error
      }

      return subscriptionStartCommand.Response();
    }

    public StopSubscriptionResponseDto StopSubscription(StoppedSubscriptionParametersDto parameters)
    {
      var subscriptionStopCommand = _commandFactory.CreateFrom(parameters);

      try
      {
        _commandProcessing.ApplyTo(subscriptionStopCommand);
      }
      catch (Exception e)
      {
        _log.Error(e);
      }

      return subscriptionStopCommand.Response();
    }
  }
}