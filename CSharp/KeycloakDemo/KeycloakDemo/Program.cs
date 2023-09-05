using System.Net;
using FluentAssertions;
using IdentityModel.Client;
using Keycloak.Net.Models.Clients;
using Keycloak.Net.Models.ClientScopes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Testcontainers.Keycloak;

namespace KeycloakDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var appFactory = new WebApplicationFactory<SecureApp.Program>()
                .WithWebHostBuilder(b => b.UseTestServer(o => o.PreserveExecutionContext = true));

            var connection = new HubConnectionBuilder().WithUrl(
                "http://localhost/echo",
                o => o.HttpMessageHandlerFactory = _ => appFactory.Server.CreateHandler()).Build();

            await connection.StartAsync();

            await using var container = new KeycloakBuilder().Build();
            await container.StartAsync();

            var keycloakClient = new Keycloak.Net.KeycloakClient(
                container.GetBaseAddress(),
                "admin",
                "admin");

            (await keycloakClient.CreateClientScopeAsync("master", new ClientScope()
            {
                Id = "scope1",
                Name = "scope1"
            })).Should().BeTrue();

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
                DefaultClientScopes = new List<string>() { "scope1", "scope2", "scope3"},
                ServiceAccountsEnabled = true,
                AuthorizationServicesEnabled = true
            })).Should().BeTrue();

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
            });

            Console.WriteLine(tokenResponse.HttpStatusCode);
            Console.WriteLine(tokenResponse.ErrorDescription ?? "no description");
            Console.WriteLine(tokenResponse.AccessToken ?? "no token");
            Console.WriteLine(tokenResponse.HttpErrorReason ?? "no reason");
        }
    }
}