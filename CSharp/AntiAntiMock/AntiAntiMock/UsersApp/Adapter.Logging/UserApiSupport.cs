using MockNoMock.UsersApp.Adapter.UserApi;

namespace MockNoMock.UsersApp.Adapter.Logging;

public class UserApiSupport : IUserApiSupport
{
  private readonly ILoggerFactory _loggerFactory;

  public UserApiSupport(ILoggerFactory loggerFactory)
  {
    _loggerFactory = loggerFactory;
  }

  public void DuplicateUserFound(string url, HttpRequestException exception, UserDto addedUser)
  {
    throw new NotImplementedException();
  }

  public IDisposable BeginCreateNewUserScope(UserDto userDto)
  {
    throw new NotImplementedException();
  }
}