using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MockNoMockSpecification._07_IntegrationOrAdapterTests;

internal class UserApiAdapterDriver : IAsyncDisposable
{
  private readonly WireMockServer _httpConfigServer;
  
  private readonly UserApiAdapter _adapter;
  private readonly IUserApiSupport _userApiSupport;

  public UserApiAdapterDriver()
  {
    _httpConfigServer = WireMockServer.Start();
    _userApiSupport = Substitute.For<IUserApiSupport>();
    _adapter = new UserApiAdapter(
      _userApiSupport, _httpConfigServer.Urls.Single());
  }

  public void ConfigServiceResponds200OkToCreating(UserDto addedUser)
  {
    _httpConfigServer
      .Given(Request.Create().UsingPost().WithUrl(_httpConfigServer.Urls.Single() + "/api/v1/users")
        .WithBody(s => JsonConvert.DeserializeObject<UserDto>(s).Equals(addedUser)))
      .RespondWith(Response.Create().WithStatusCode(200));
  }

  public void ConfigServiceResponds409ConflictToCreating(UserDto addedUser)
  {
    _httpConfigServer
      .Given(Request.Create().UsingPost().WithUrl(ApiV1Users)
        .WithBody(s => JsonConvert.DeserializeObject<UserDto>(s).Equals(addedUser)))
      .RespondWith(Response.Create().WithStatusCode(409));
  }

  private string ApiV1Users => _httpConfigServer.Urls.Single() + "/api/v1/users";

  public async Task CreateNewUser(UserDto addedUser)
  {
    await _adapter.UserApi.CreateNewUser(addedUser);
  }

  public async ValueTask DisposeAsync()
  {
    _httpConfigServer.Dispose();
    await Task.CompletedTask;
  }

  public void LogsShouldContainErrorAboutDuplicateUser(UserDto addedUser)
  {
    _userApiSupport.Received(1).DuplicateUserFound(ApiV1Users, Arg.Any<DuplicateUserException>(), addedUser);
  }
}