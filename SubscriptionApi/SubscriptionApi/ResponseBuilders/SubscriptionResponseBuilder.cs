using System;
using System.Collections.Generic;
using System.Linq;
using SubscriptionApi.Dto;

namespace SubscriptionApi.ResponseBuilders
{
  public class SubscriptionResponseBuilder : 
    SubscriptionStopResponseBuilder, SubscriptionStartResponseBuilder
  {
    private readonly List<string> _errors = new List<string>();

    public void AssertNoFatalErrors(Exception exceptionToThrow)
    {
      if (_errors.Any())
      {
        throw exceptionToThrow;
      }
    }

    public void NotAuthorized(string assetName, string userName)
    {
      _errors.Add(userName + " has no rights to " + assetName);
    }

    public void NotValid(string dataName)
    {
      _errors.Add(dataName + " is invalid");
    }

    StartSubscriptionResponseDto SubscriptionStartResponseBuilder.Build()
    {
      return new StartSubscriptionResponseDto()
      {
        Failure = _errors.Any(),
        Errors = _errors.ToArray()
      };
    }
    public StopSubscriptionResponseDto Build()
    {
      return new StopSubscriptionResponseDto()
      {
        Failure = _errors.Any(),
        Errors = _errors.ToArray()
      };
    }

    public void NoResolutionResultsFor(string name)
    {
      _errors.Add("Resolution of " + name + " yielded no results");
    }

    public void NoSubscriptionToStopWithId(string subscriptionId)
    {
      _errors.Add("No subscription with id " + subscriptionId + " found");
    }

    public void UserNotAuthorized(string userName)
    {
      _errors.Add("User " + userName + " could not be authorized in the system");
    }

    public void NotAuthorizedForAsset(string assetName, string userName)
    {
      _errors.Add("User " + userName + " is not authorized for " + assetName);
    }
  }

}