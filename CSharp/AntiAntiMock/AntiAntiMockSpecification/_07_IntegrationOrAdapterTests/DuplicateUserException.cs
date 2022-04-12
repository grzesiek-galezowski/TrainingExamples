using System;

namespace MockNoMockSpecification._07_IntegrationOrAdapterTests;

public class DuplicateUserException : Exception
{
  public DuplicateUserException(Exception exception)
  : base("Attempting to create a user who already exists", exception)
  {
    
  }
}