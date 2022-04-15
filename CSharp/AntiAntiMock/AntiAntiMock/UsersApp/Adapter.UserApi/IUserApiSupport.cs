namespace MockNoMock.UsersApp.Adapter.UserApi;

public interface IUserApiSupport
{
  void DuplicateUserFound(string url, HttpRequestException exception, UserDto addedUser);
  IDisposable BeginCreateNewUserScope(UserDto userDto);
}