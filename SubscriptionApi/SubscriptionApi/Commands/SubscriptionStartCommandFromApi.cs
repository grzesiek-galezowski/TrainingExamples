using System.Collections.Generic;
using SubscriptionApi.Dto;
using SubscriptionApi.Exceptions;
using SubscriptionApi.Queries;
using SubscriptionApi.ResponseBuilders;
using SubscriptionApi.Subscriptions;

namespace SubscriptionApi.Commands
{
  public interface SubscriptionStartCommand
  {
    void ValidateData();
    void Resolve();
    void Authorize();
    void Invoke();
    StartSubscriptionResponseDto Response();
  }

  public class SubscriptionStartCommandFromApi : SubscriptionStartCommand
  {
    private readonly NewSubscriptionParametersDto _parameters;
    private readonly List<string> _requestedAssetNames = new List<string>();
    private readonly AssetAccessAuthorization _authorizationStructure;
    private readonly SubscriptionStartResponseBuilder _responseBuilder;
    private readonly ISubscriptionFactory _subscriptionFactory;
    private readonly List<AssetQuery> _assetQueries = new List<AssetQuery>();
    private readonly SubscriptionsModifyOperations _subscriptions;
    

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

      throw new System.NotImplementedException();
    }

    public void Resolve()
    {
      foreach (var query in _assetQueries)
      {
        query.ResolveInto(_requestedAssetNames, _responseBuilder);
      }

      _responseBuilder.AssertNoFatalErrors(new FatalErrorOccuredDuringQueryResolutionException());
    }

    public void Authorize()
    {
      foreach (var assetName in _requestedAssetNames)
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
      var subscription = _subscriptionFactory.CreateFrom(_parameters);
      _subscriptions.AddNew(subscription);
    }


    public StartSubscriptionResponseDto Response()
    {
      return _responseBuilder.Build();
    }
  }

  //Add Subscription.SendNotification()
}