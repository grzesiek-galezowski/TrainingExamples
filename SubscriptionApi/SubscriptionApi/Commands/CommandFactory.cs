using System.Collections.Generic;
using SubscriptionApi.Authorization;
using SubscriptionApi.Dto;
using SubscriptionApi.ResponseBuilders;
using SubscriptionApi.Subscriptions;

namespace SubscriptionApi.Commands
{
  public interface ICommandFactory
  {
    SubscriptionStartCommand CreateFrom(NewSubscriptionParametersDto parameters);
    SubscriptionStopCommand CreateFrom(StoppedSubscriptionParametersDto parameters);
  }

  public class CommandFactory : ICommandFactory
  {
    private readonly SubscriptionResponseBuilder _responseBuilder;
    private readonly SubscriptionsModifyOperations _subscriptions;
    private readonly AuthorizationStructure _authorizationStructure;
    private readonly ISubscriptionFactory _subscriptionFactory;
    private readonly IAssetQueriesFactory _assetQueriesFactory;
    private readonly SubscriptionDataCorrectnessCriteria _dataCorrectnessCriteria;

    public CommandFactory(
      SubscriptionResponseBuilder responseBuilder, 
      SubscriptionsModifyOperations subscriptions, 
      AuthorizationStructure authorizationStructure, 
      ISubscriptionFactory subscriptionFactory, 
      SubscriptionDataCorrectnessCriteria dataCorrectnessCriteria, 
      IAssetQueriesFactory assetQueriesFactory)
    {
      _responseBuilder = responseBuilder;
      _subscriptions = subscriptions;
      _authorizationStructure = authorizationStructure;
      _subscriptionFactory = subscriptionFactory;
      _dataCorrectnessCriteria = dataCorrectnessCriteria;
      _assetQueriesFactory = assetQueriesFactory;
    }

    public SubscriptionStartCommand CreateFrom(NewSubscriptionParametersDto parameters)
    {
      var assetQueries = _assetQueriesFactory.CreateFrom(parameters.Requests);
      return new SubscriptionStartCommandFromApi(
        parameters, 
        _authorizationStructure, 
        _responseBuilder, 
        _subscriptionFactory, 
        _subscriptions,
        assetQueries);
    }

    public SubscriptionStopCommand CreateFrom(StoppedSubscriptionParametersDto parameters)
    {
      return new SubscriptionStopCommandFromApi(
        parameters, 
        _responseBuilder, 
        _subscriptions, 
        _dataCorrectnessCriteria, 
        _authorizationStructure);
    }
  }
}