using System.Threading.Tasks;
using WireMock.Server;

namespace MockNoMockSpecification._07_IntegrationOrAdapterTests;

internal class UserApiAdapterTests
{
  [Test]
  public async Task ShouldXXXXXXXXXXXXX() //bug
  {
    using var httpConfigServer = WireMockServer.Start();

    //GIVEN
    var adapter = new UserApiAdapter(
      Substitute.For<IUserApiSupport>(),
      "some kind of url");

    //WHEN
    await adapter.UserApi.CreateNewUser(new UserDto("Zenek", "Kopytko"));

    //THEN
    Assert.Fail("unfinished");
  }

}

internal class UserApiAdapter
{
  public UserApiAdapter(IUserApiSupport support, string uri)
  {
    UserApi = new HttpBasedUserApi(support, uri);
  }

  public IHttpBasedUserApi UserApi { get; }
}

internal interface IHttpBasedUserApi
{
  Task CreateNewUser(UserDto userDto);
}

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
    throw new System.NotImplementedException();
  }
}

internal interface IUserApiSupport
{
}

public readonly record struct UserDto(string Name, string Surname);