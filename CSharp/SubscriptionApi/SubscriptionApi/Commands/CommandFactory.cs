using System.Collections.Generic;
using SubscriptionApi.Authorization;
using SubscriptionApi.Dto;
using SubscriptionApi.ResponseBuilders;
using SubscriptionApi.Subscriptions;

namespace SubscriptionApi.Commands
{
  public interface ICommandFactory
  {
    Command CreateFrom(NewSubscriptionParametersDto parameters, SubscriptionStartResponseBuilder responseBuilder);
    Command CreateFrom(StoppedSubscriptionParametersDto parameters, SubscriptionStopResponseBuilder responseBuilder);
  }

  public class CommandFactory : ICommandFactory
  {
    private readonly SubscriptionsModifyOperations _subscriptions;
    private readonly AuthorizationStructure _authorizationStructure;
    private readonly ISubscriptionFactory _subscriptionFactory;
    private readonly IAssetQueriesFactory _assetQueriesFactory;
    private readonly Log _log;
    private readonly SubscriptionDataCorrectnessCriteria _dataCorrectnessCriteria;

    public CommandFactory(
      SubscriptionsModifyOperations subscriptions, 
      AuthorizationStructure authorizationStructure, 
      ISubscriptionFactory subscriptionFactory, 
      SubscriptionDataCorrectnessCriteria dataCorrectnessCriteria, 
      IAssetQueriesFactory assetQueriesFactory, 
      Log log)
    {
      _subscriptions = subscriptions;
      _authorizationStructure = authorizationStructure;
      _subscriptionFactory = subscriptionFactory;
      _dataCorrectnessCriteria = dataCorrectnessCriteria;
      _assetQueriesFactory = assetQueriesFactory;
      _log = log;
    }

    public Command CreateFrom(NewSubscriptionParametersDto parameters, SubscriptionStartResponseBuilder responseBuilder)
    {
      var assetQueries = _assetQueriesFactory.CreateFrom(parameters.Requests);
      return 
        new ExceptionLoggedCommand(_log,
          new AdapterFromSubscriptionCommandToCommand(
            new SubscriptionStartCommandFromApi(
              parameters, 
              _authorizationStructure, 
              responseBuilder, 
              _subscriptionFactory, 
              _subscriptions,
              assetQueries)));
    }

    public Command CreateFrom(StoppedSubscriptionParametersDto parameters, SubscriptionStopResponseBuilder responseBuilder)
    {
      return 
        new ExceptionLoggedCommand(_log,
          new AdapterFromSubscriptionCommandToCommand(
            new SubscriptionStopCommandFromApi(
              parameters, 
              responseBuilder, 
              _subscriptions, 
              _dataCorrectnessCriteria, 
              _authorizationStructure)));
    }
  }
}