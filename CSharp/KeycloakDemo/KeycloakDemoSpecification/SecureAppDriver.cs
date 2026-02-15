using FluentAssertions;
using GraphQL.Types.Relay.DataObjects;
using IdentityModel.Client;
using KeycloakMock;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using SecureApp;
using System.Net;

namespace KeycloakDemoSpecification;

public class SecureAppDriver : IAsyncDisposable
{
  public readonly KeycloakMockServer? _mockServer;
  public readonly WebApplicationFactory<Program>? _appFactory;
  public HttpClient? _httpClient;
  public DiscoveryDocumentResponse? _discoveryDocument;
  private HubConnection _connection;

  private SecureAppDriver(KeycloakMockServer keycloakMockServer, WebApplicationFactory<Program> hostBuilder)
  {
    _mockServer = keycloakMockServer;
    _appFactory = hostBuilder;

  }

  public static SecureAppDriver Create()
  {
    var keycloakMockServer = KeycloakMockServer.Start(
      port: 8080,
      useSsl: false,
      audience: "account",
      realm: "master");
    return new SecureAppDriver(
      keycloakMockServer,
      new WebApplicationFactory<SecureApp.Program>()
        .WithWebHostBuilder(b =>
        {
          b.UseTestServer(o => o.PreserveExecutionContext = true);
          b.ConfigureAppConfiguration((context, builder) =>
          {
            builder.AddInMemoryCollection(new Dictionary<string, string?>()
            {
              ["Keycloak:Authority"] = keycloakMockServer.Authority,
              ["Keycloak:Audience"] = keycloakMockServer.Audience,
              ["Keycloak:RequireHttpsMetadata"] = "false"
            });
          });
        }));
  }

  public void Start()
  {
    _httpClient = _appFactory.CreateClient();
  }

  public async Task ConnectToHub(string token)
  {
    _connection = new HubConnectionBuilder()
      .WithUrl(
        "http://localhost/echo",
        o =>
        {
          o.HttpMessageHandlerFactory = _ => _appFactory!.Server.CreateHandler();
          o.AccessTokenProvider = async () => token;
          o.WebSocketFactory = async (context, cancellationToken) =>
          {
            var wsClient = _appFactory!.Server.CreateWebSocketClient();
            var uri = new UriBuilder(context.Uri);
            return await wsClient.ConnectAsync(uri.Uri, cancellationToken);
          };
        })
      .Build();

    await _connection.StartAsync();
    _connection.State.Should().Be(HubConnectionState.Connected);
  }

  public async ValueTask DisposeAsync()
  {
    if (_mockServer != null) await CastAndDispose(_mockServer);
    if (_appFactory != null) await _appFactory.DisposeAsync();
    if (_httpClient != null) await CastAndDispose(_httpClient);
    await (_connection?.DisposeAsync() ?? ValueTask.CompletedTask);

    return;

    static async ValueTask CastAndDispose(IDisposable resource)
    {
      if (resource is IAsyncDisposable resourceAsyncDisposable)
        await resourceAsyncDisposable.DisposeAsync();
      else
        resource.Dispose();
    }
  }
}