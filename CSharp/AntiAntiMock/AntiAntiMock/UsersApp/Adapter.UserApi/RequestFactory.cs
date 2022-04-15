using Flurl.Http;

namespace MockNoMock.UsersApp.Adapter.UserApi;

internal class RequestFactory
{
  private readonly string _baseUrl;

  public RequestFactory(string baseUrl)
  {
    _baseUrl = baseUrl;
  }

  public FlurlRequest CreateUserRequest()
  {
    return new FlurlRequest(_baseUrl + "/api/v1/users");
  }
}