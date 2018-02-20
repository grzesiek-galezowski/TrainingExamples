package responseBuilders;

import dto.StartSubscriptionResponseDto;
import queries.QueryResolutionEvents;

public interface SubscriptionStartResponseBuilder extends
    SubscriptionValidationResults,
    QueryResolutionEvents,
    AssetAuthorizationEvents {
    StartSubscriptionResponseDto buildStart();
    void assertNoFatalErrors(RuntimeException exceptionToThrow);
}
