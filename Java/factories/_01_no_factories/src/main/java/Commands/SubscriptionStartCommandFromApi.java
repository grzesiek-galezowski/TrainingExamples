package Commands;

import Dto.NewSubscriptionParametersDto;
import Dto.StartSubscriptionResponseDto;
import Exceptions.FatalErrorOccuredDuringAuthorizationException;
import Exceptions.FatalErrorOccuredDuringQueryResolutionException;
import Queries.AssetQuery;
import ResponseBuilders.SubscriptionStartResponseBuilder;
import Subscriptions.ISubscriptionFactory;
import Subscriptions.Subscription;
import Subscriptions.SubscriptionsModifyOperations;

import java.util.ArrayList;
import java.util.List;

public class SubscriptionStartCommandFromApi implements SubscriptionCommand
  {
    private final NewSubscriptionParametersDto _parameters;
    private final List<String> _requestedAssetNames = new ArrayList<String>();
    private final AssetAccessAuthorization _authorizationStructure;
    private final SubscriptionStartResponseBuilder _responseBuilder;
    private final ISubscriptionFactory _subscriptionFactory;
    private final List<AssetQuery> _assetQueries;
    private final SubscriptionsModifyOperations _subscriptions;


    public SubscriptionStartCommandFromApi(
      NewSubscriptionParametersDto parameters,
      AssetAccessAuthorization authorizationStructure,
      SubscriptionStartResponseBuilder responseBuilder,
      ISubscriptionFactory subscriptionFactory,
      SubscriptionsModifyOperations subscriptions,
      List<AssetQuery> assetQueries)
    {
      _parameters = parameters;
      _authorizationStructure = authorizationStructure;
      _responseBuilder = responseBuilder;
      _subscriptionFactory = subscriptionFactory;
      _subscriptions = subscriptions;
      _assetQueries = assetQueries;
    }

    public void ValidateData()
    {
      //TODO user name
      //TODO duration
      //TODO validate each query

      throw new RuntimeException("not implemented");
    }

    public void Resolve()
    {
      for (AssetQuery query : _assetQueries)
      {
        query.ResolveInto(_requestedAssetNames, _responseBuilder);
      }

      _responseBuilder.AssertNoFatalErrors(
          new FatalErrorOccuredDuringQueryResolutionException());
    }

    public void Authorize()
    {
      for (String assetName : _requestedAssetNames)
      {
        _authorizationStructure.VerifyAccessTo(
          assetName,
          _parameters.UserName,
          _responseBuilder);
      }

      _responseBuilder.AssertNoFatalErrors(
        new FatalErrorOccuredDuringAuthorizationException());
    }

    public void Invoke()
    {
      Subscription subscription = _subscriptionFactory.CreateFrom(_parameters);
      _subscriptions.AddNew(subscription);
    }


    public StartSubscriptionResponseDto Response()
    {
      return _responseBuilder.BuildStart();
    }
  }
