using System;
using SubscriptionApi.Dto;
using SubscriptionApi.Queries;

namespace SubscriptionApi.ResponseBuilders
{
  public interface SubscriptionStartResponseBuilder : 
    SubscriptionValidationResults, 
    QueryResolutionEvents, 
    AssetAuthorizationEvents
  {
    StartSubscriptionResponseDto Build();
    void AssertNoFatalErrors(Exception exceptionToThrow);
  }
}