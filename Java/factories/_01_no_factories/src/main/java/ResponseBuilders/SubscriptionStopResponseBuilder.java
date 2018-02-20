package responseBuilders;

import dto.StopSubscriptionResponseDto;

public interface SubscriptionStopResponseBuilder extends
    SubscriptionValidationResults,
    SubscriptionStopEvents,
    UserAuthorizationEvents
  {
    StopSubscriptionResponseDto buildStop();
    void assertNoFatalErrors(RuntimeException exception);
  }
