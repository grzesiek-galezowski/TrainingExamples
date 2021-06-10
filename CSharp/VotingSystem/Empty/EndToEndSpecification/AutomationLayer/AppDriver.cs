using System;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ApplicationLogic.Ports;
using Bootstrap.CompositionRoot;
using Lib;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace EndToEndSpecification.AutomationLayer
{
  public class AppDriver : IDisposable
  {
    private TestServer _testServer;
    private IHostBuilder _hostBuilder;

    public AppDriver()
    {
        _hostBuilder = Host
            .CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .UseEnvironment("Development")
                    .UseStartup<Startup>();
            });
    }

    public async Task Start()
    {
        _testServer = (await _hostBuilder.StartAsync()).GetTestServer();
        _testServer.AllowSynchronousIO = true;
    }

    public async Task<CreateUserResponse> TryToCreate(UserDtoBuilder userDtoBuilder)
    {
      using (var client = _testServer.CreateClient())
      {
        var response = await client.PostAsJsonAsync("api/users", userDtoBuilder.Build());
        var content = await response.Content.ReadAsStringAsync();
        return new CreateUserResponse(content, response.StatusCode, response.Headers);
      }
    }

    public void Dispose()
    {
      _testServer?.Dispose();
    }

    public async Task<GetUserResponse> TryToGet(UserDtoBuilder user)
    {
      using (var client = _testServer.CreateClient())
      {
        var result = await client.GetAsync("api/users" + $"/{user.Build().Login}");
        return new GetUserResponse(await result.Content.ReadAsAsync<UserDto>(), result.StatusCode);
      }
    }

    public async Task<CreateUserResponse> Create(UserDtoBuilder userDto)
    {
      var createJohnnyResponse = await TryToCreate(userDto);
      createJohnnyResponse.ShouldIndicateSuccessfulCreationOf(userDto);
      return createJohnnyResponse;
    }

    public async Task<GetUserResponse> Get(UserDtoBuilder user)
    {
      var getJohnnyResponse = await TryToGet(user);
      getJohnnyResponse.ShouldIndicateSuccess();
      return getJohnnyResponse;
    }
  }
}