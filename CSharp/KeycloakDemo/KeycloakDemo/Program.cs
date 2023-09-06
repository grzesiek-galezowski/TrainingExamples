using System.Net;
using FluentAssertions;
using IdentityModel.Client;
using Keycloak.Net.Models.Clients;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Testcontainers.Keycloak;

namespace KeycloakDemo;

internal class Program
{
  static async Task Main(string[] args)
  {
    await using var container = new KeycloakBuilder().Build();
    await container.StartAsync();

    var appFactory = new WebApplicationFactory<SecureApp.Program>()
        .WithWebHostBuilder(b =>
        {
          b.UseTestServer(o => o.PreserveExecutionContext = true);
          b.ConfigureAppConfiguration((context, builder) =>
          {
            builder.AddInMemoryCollection(new Dictionary<string, string?>()
            {
              ["Keycloak:Authority"] = container.GetBaseAddress() + "realms/master",
              ["Keycloak:Audience"] = "account",
              ["Keycloak:RequireHttpsMetadata"] = "false"
            });
          });
        });
    
    Console.WriteLine(container.GetBaseAddress());

    var keycloakClient = new Keycloak.Net.KeycloakClient(
      container.GetBaseAddress(),
      "admin",
      "admin");

    (await keycloakClient.CreateClientAsync("master", new Client()
    {
      ClientId = "zenek",
      Access = new ClientAccess()
      {
        Configure = true,
        Manage = true,
        View = true
      },
      Enabled = true,
      Secret = "zenon",
      FullScopeAllowed = true,
      DefaultClientScopes = new List<string>()
      {
        "openid", "acr",
        "address",
        "email",
        "profile",
        "roles",
        "web-origins",
      },
      OptionalClientScopes = new List<string>()
      {
        "microprofile-jwt Optional",
        "offline_access Optional",
        "phone Optional",
      },
      ServiceAccountsEnabled = true,
      AuthorizationServicesEnabled = true,
    })).Should().BeTrue();


    //await keycloakClient.CreateProtocolMapperAsync("master", "openid", new ProtocolMapper()
    //{
    //  ConsentRequired = false,
    //  Name = "some protocol mapper",
    //  Id = "Audience1",
    //  Protocol = "Audience2",
    //  Config = new Dictionary<string, string>()
    //  {
    //    ["a"] = "b"
    //  },
    //  _ProtocolMapper = "SomeMapper"
    //});



    using var httpClient = new HttpClient();

    var response = await httpClient.GetDiscoveryDocumentAsync(
      container.GetBaseAddress() + "realms/master");
    if (response.HttpStatusCode != HttpStatusCode.OK)
      throw new Exception(response.HttpErrorReason);
    if (response.IsError)
      throw new Exception(response.ErrorType + " " + response.Error);

    var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(
      new ClientCredentialsTokenRequest()
      {
        Address = response.TokenEndpoint,
        ClientId = "zenek",
        ClientSecret = "zenon",
        Scope = "openid",
        Parameters = new Parameters()
      });

    Console.WriteLine(tokenResponse.HttpStatusCode);
    Console.WriteLine(tokenResponse.ErrorDescription ?? "no description");
    Console.WriteLine(tokenResponse.AccessToken ?? "no token");
    Console.WriteLine(tokenResponse.HttpErrorReason ?? "no reason");

    var connection = new HubConnectionBuilder().WithUrl(
      $"http://localhost/echo?access_token={tokenResponse.AccessToken}",
      o => o.HttpMessageHandlerFactory = _ => appFactory.Server.CreateHandler()).Build();
    await connection.StartAsync();

  }
}