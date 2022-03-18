using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AntiAntiMock;
using FluentAssertions;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace AntiAntiMockSpecification;

public class ChangeDetectors
{
  [Test]
  public async Task ShouldCallHttpMock1ThenHttpMock2()
  {
    //GIVEN
    using var mock1 = WireMockServer.Start();
    mock1.Given(Request.Create())
      .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
    using var mock2 = WireMockServer.Start();
    mock2.Given(Request.Create())
      .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
    var mock1Url = mock1.Urls.Single() + "/";
    var mock2Url = mock2.Urls.Single() + "/";

    await using var app = new WebApplicationFactory<Program>()
      .WithWebHostBuilder(
        builder => builder.ConfigureAppConfiguration((_, configurationBuilder)
          => configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>
          {
            ["Urls:Url1"] = mock1Url, ["Urls:Url2"] = mock2Url,
          })));
    using var client = new FlurlClient(app.CreateClient());

    //WHEN
    await client.Request("/broadcast").PostJsonAsync(new Work());

    //THEN
    var logEntries = mock1.LogEntries.Concat(mock2.LogEntries).OrderBy(entry => entry.RequestMessage.DateTime).ToList();
    logEntries[0].RequestMessage.AbsoluteUrl.Should().Be(mock1Url);
    logEntries[1].RequestMessage.AbsoluteUrl.Should().Be(mock2Url);
  }

  [Test]
  public void ShouldCallMock1ThenMock2()
  {
    //GIVEN
    var mock1 = Substitute.For<IRecipient>();
    var mock2 = Substitute.For<IRecipient>();
    var broadcast = new Broadcast(mock1, mock2);
    var work = new Work();
    
    //WHEN
    broadcast.MakeFor(work);

    //THEN
    Received.InOrder(() =>
    {
      mock1.Handle(work);
      mock2.Handle(work);
    });
  }
}