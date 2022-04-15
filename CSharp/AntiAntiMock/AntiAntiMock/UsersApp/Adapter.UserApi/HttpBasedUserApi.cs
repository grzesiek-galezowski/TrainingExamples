using System.Net;
using Flurl.Http;

namespace MockNoMock.UsersApp.Adapter.UserApi;

internal class HttpBasedUserApi : IUserApi
{
  private readonly IUserApiSupport _support;
  private readonly RequestFactory _requestFactory;

  public HttpBasedUserApi(IUserApiSupport support, RequestFactory requestFactory)
  {
    _support = support;
    _requestFactory = requestFactory;
  }

  public async Task CreateNewUser(UserDto userDto)
  {
    var request = _requestFactory.CreateUserRequest();
    try
    {
      var response = await request.AllowAnyHttpStatus().PostJsonAsync(userDto);
      response.ResponseMessage.EnsureSuccessStatusCode();
    }
    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
    {
      _support.DuplicateUserFound(request.Url, ex, userDto);
      throw new DuplicateUserException(ex);
    }
  }
}