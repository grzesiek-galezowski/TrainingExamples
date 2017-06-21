using System;
using SubscriptionApi.Dto;

namespace SubscriptionApi.ResponseBuilders
{
  public interface SubscriptionStopResponseBuilder : 
    SubscriptionValidationResults, 
    SubscriptionStopEvents,
    UserAuthorizationEvents
  {
    StopSubscriptionResponseDto Build();
    void AssertNoFatalErrors(Exception exception);
  }
}