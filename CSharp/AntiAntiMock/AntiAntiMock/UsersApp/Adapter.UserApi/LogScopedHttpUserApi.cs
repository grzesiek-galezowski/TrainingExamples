namespace MockNoMock.UsersApp.Adapter.UserApi;

internal class LogScopedHttpUserApi : IUserApi
{
  private readonly IUserApiSupport _userApiSupport;
  private readonly IUserApi _inner;

  public LogScopedHttpUserApi(IUserApiSupport userApiSupport, IUserApi inner)
  {
    _userApiSupport = userApiSupport;
    _inner = inner;
  }

  public async Task CreateNewUser(UserDto userDto)
  {
    using var scope = _userApiSupport.BeginCreateNewUserScope(userDto);
    await _inner.CreateNewUser(userDto);
  }
}