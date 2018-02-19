package ResponseBuilders;

import Dto.StartSubscriptionResponseDto;
import Queries.QueryResolutionEvents;

public interface SubscriptionStartResponseBuilder extends
    SubscriptionValidationResults,
    QueryResolutionEvents,
    AssetAuthorizationEvents {
    StartSubscriptionResponseDto BuildStart();
    void AssertNoFatalErrors(RuntimeException exceptionToThrow);
}
