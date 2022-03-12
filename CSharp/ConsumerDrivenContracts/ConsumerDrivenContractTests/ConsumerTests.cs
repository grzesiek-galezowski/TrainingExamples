using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using PactNet;

namespace ConsumerDrivenContractTests;

public class ConsumerTests
{
    public record ResponseDto(string Result);

    [Test]
    public async Task GetSomething_WhenTheTesterSomethingExists_ReturnsTheSomething()
    {
        var pact = Pact.V3("Something API Consumer", "Something API");
        var mock = pact.UsingNativeBackend();

        // Arrange
        mock
            .UponReceiving("A test GET")
            .WithRequest(HttpMethod.Get, "/test")
            .WithHeader("Accept", "application/json")
            .WillRespond()
            .WithStatus(HttpStatusCode.OK)
            .WithHeader("Content-Type", "application/json")
            .WithJsonBody(new ResponseDto("Awright!"));

        await mock.VerifyAsync(async ctx => //zaczynamy nagrywanie
        {
            // Act
            var response = await ApkaKonsument.WykonajCoś(ctx);

            // Assert
            response.Should().Be(new ResponseDto("Awright!"));
        });
    }
}