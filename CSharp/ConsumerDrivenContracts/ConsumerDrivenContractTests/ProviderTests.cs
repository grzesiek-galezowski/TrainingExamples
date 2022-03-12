using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using PactNet.Infrastructure.Outputters;
using PactNet.Verifier;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Request = WireMock.RequestBuilders.Request;

namespace ConsumerDrivenContractTests;

public class TestApiProvider : IDisposable
{
    private readonly WireMockServer _wireMockServer;
    public string ServerUri { get; }

    public TestApiProvider()
    {
        //prawidziwy server asp.net core z kontrolerami itp.
        _wireMockServer = WireMockServer.Start();
        ServerUri = _wireMockServer.Urls.Single();
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath("/test"))
            .RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new ConsumerTests.ResponseDto("Awright!"))
                    .WithHeader("Content-Type", "application/json"));
    }

    public void Dispose()
    {
        _wireMockServer.Dispose();
    }
}

public class SomethingApiTests
{

    [Test]
    public void EnsureSomethingApiHonoursPactWithConsumer()
    {
        var producerApp = new TestApiProvider();
        //Arrange

        // Act / Assert
        new PactVerifier(new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new ConsoleOutput()
                },
            })
            .ServiceProvider("Something API", new Uri(producerApp.ServerUri))
            .WithFileSource(new FileInfo(
                Path.Combine("../../../pacts/Something API Consumer-Something API.json")))
            .Verify();
    }
}