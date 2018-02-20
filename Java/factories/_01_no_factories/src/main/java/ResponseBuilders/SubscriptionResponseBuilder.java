package responseBuilders;

import dto.StartSubscriptionResponseDto;
import dto.StopSubscriptionResponseDto;

import java.util.ArrayList;
import java.util.List;

public class SubscriptionResponseBuilder implements
    SubscriptionStopResponseBuilder, SubscriptionStartResponseBuilder {
    private final List<String> errors = new ArrayList<>();

    public void assertNoFatalErrors(RuntimeException exceptionToThrow) {
        if (!errors.isEmpty()) {
            throw exceptionToThrow;
        }
    }

    public void notAuthorized(String assetName, String userName) {
        errors.add(userName + " has no rights to " + assetName);
    }

    public void notValid(String dataName) {
        errors.add(dataName + " is invalid");
    }

    public StartSubscriptionResponseDto buildStart() //todo rename to build start
    {
        StartSubscriptionResponseDto dto = new StartSubscriptionResponseDto();
        dto.failure = !errors.isEmpty();
        dto.errors = errors.stream().toArray(String[]::new);
        return dto;
    }

    public StopSubscriptionResponseDto buildStop() {
        StopSubscriptionResponseDto dto = new StopSubscriptionResponseDto();
        dto.failure = !errors.isEmpty();
        dto.errors = errors.stream().toArray(String[]::new);
        return dto;
    }

    public void noResolutionResultsFor(String name) {
        errors.add("Resolution of " + name + " yielded no results");
    }

    public void noSubscriptionToStopWithId(String subscriptionId) {
        errors.add("No subscription with id " + subscriptionId + " found");
    }

    public void userNotAuthorized(String userName) {
        errors.add("User " + userName + " could not be authorized in the system");
    }

    public void notAuthorizedForAsset(String assetName, String userName) {
        errors.add("User " + userName + " is not authorized for " + assetName);
    }
}

