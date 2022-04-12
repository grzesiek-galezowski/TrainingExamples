namespace MockNoMockSpecification._07_IntegrationOrAdapterTests;

public interface IUserApiSupport
{
  void DuplicateUserFound(string url, DuplicateUserException exception, UserDto addedUser);
}