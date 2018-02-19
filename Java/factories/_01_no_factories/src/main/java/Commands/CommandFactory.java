package Commands;

import Authorization.AuthorizationStructure;
import Dto.NewSubscriptionParametersDto;
import Dto.StoppedSubscriptionParametersDto;
import Queries.AssetQuery;
import ResponseBuilders.SubscriptionStartResponseBuilder;
import ResponseBuilders.SubscriptionStopResponseBuilder;
import Subscriptions.ISubscriptionFactory;
import Subscriptions.SubscriptionDataCorrectnessCriteria;
import Subscriptions.SubscriptionsModifyOperations;
import other.Log;

import java.util.List;

import static java.util.Arrays.asList;

public class CommandFactory implements ICommandFactory
  {
    private final SubscriptionsModifyOperations _subscriptions;
    private final AuthorizationStructure _authorizationStructure;
    private final ISubscriptionFactory _subscriptionFactory;
    private final IAssetQueriesFactory _assetQueriesFactory;
    private final Log _log;
    private final SubscriptionDataCorrectnessCriteria _dataCorrectnessCriteria;

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
      List<AssetQuery> assetQueries = _assetQueriesFactory
          .CreateFrom(asList(parameters.Requests));
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
