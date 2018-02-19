package ResponseBuilders;

import Dto.StartSubscriptionResponseDto;
import Dto.StopSubscriptionResponseDto;
import ResponseBuilders.SubscriptionStartResponseBuilder;
import ResponseBuilders.SubscriptionStopResponseBuilder;

import java.util.ArrayList;
import java.util.List;

public class SubscriptionResponseBuilder implements
    SubscriptionStopResponseBuilder, SubscriptionStartResponseBuilder
  {
    private final List<String> _errors = new ArrayList<>();

    public void AssertNoFatalErrors(RuntimeException exceptionToThrow)
    {
      if (!_errors.isEmpty())
      {
        throw exceptionToThrow;
      }
    }

    public void NotAuthorized(String assetName, String userName)
    {
      _errors.add(userName + " has no rights to " + assetName);
    }

    public void NotValid(String dataName)
    {
      _errors.add(dataName + " is invalid");
    }

    public StartSubscriptionResponseDto BuildStart() //todo rename to build start
    {
      StartSubscriptionResponseDto dto = new StartSubscriptionResponseDto();
      dto.Failure = !_errors.isEmpty();
      dto.Errors = _errors.stream().toArray(String[]::new);
      return dto;
    }

    public StopSubscriptionResponseDto BuildStop()
    {
      return new StopSubscriptionResponseDto()
      {
        Failure = _errors.Any(),
        Errors = _errors.ToArray()
      };
    }

    public void NoResolutionResultsFor(String name)
    {
      _errors.add("Resolution of " + name + " yielded no results");
    }

    public void NoSubscriptionToStopWithId(String subscriptionId)
    {
      _errors.add("No subscription with id " + subscriptionId + " found");
    }

    public void UserNotAuthorized(String userName)
    {
      _errors.add("User " + userName + " could not be authorized in the system");
    }

    public void NotAuthorizedForAsset(String assetName, String userName)
    {
      _errors.add("User " + userName + " is not authorized for " + assetName);
    }
  }

