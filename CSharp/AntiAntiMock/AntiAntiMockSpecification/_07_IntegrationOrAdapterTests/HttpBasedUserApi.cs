using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;

namespace MockNoMockSpecification._07_IntegrationOrAdapterTests;

internal class HttpBasedUserApi : IHttpBasedUserApi
{
  private readonly IUserApiSupport _support;
  private readonly string _uri;

  public HttpBasedUserApi(IUserApiSupport support, string uri)
  {
    _support = support;
    _uri = uri;
  }

  public async Task CreateNewUser(UserDto userDto)
  {
    try
    {
      var response = await (_uri + "/api/v1/users").AllowAnyHttpStatus().PostJsonAsync(userDto);
      response.ResponseMessage.EnsureSuccessStatusCode();
    }
    catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
    {
      throw new DuplicateUserException(ex);
    }
  }
}