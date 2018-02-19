package ResponseBuilders;

import Dto.StopSubscriptionResponseDto;

public interface SubscriptionStopResponseBuilder extends
    SubscriptionValidationResults,
    SubscriptionStopEvents,
    UserAuthorizationEvents
  {
    StopSubscriptionResponseDto BuildStop();
    void AssertNoFatalErrors(RuntimeException exception);
  }
