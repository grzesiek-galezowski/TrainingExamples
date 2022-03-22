using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using MockNoMock;
using NSubstitute;
using NUnit.Framework;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MockNoMockSpecification._01_ChangeDetectors;

public class _02_MocksVsHttpMocks
{
  [Test]
  public void ShouldPassTheWorkToBothRecipientsInOrder()
  {
    //GIVEN
    var recipient1 = Substitute.For<IRecipient>();
    var recipient2 = Substitute.For<IRecipient>();
    var broadcast = new Broadcast(recipient1, recipient2);
    var work = new Work();

    //WHEN
    broadcast.MakeFor(work);

    //THEN
    Received.InOrder(() =>
    {
      recipient1.Handle(work);
      recipient2.Handle(work);
    });
  }

  [Test]
  public async Task ShouldPassTheWorkToBothRecipientsInOrderViaHttp()
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
          => configurationBuilder.AddInMemoryCollection(
            new Dictionary<string, string> { ["Urls:Url1"] = mock1Url, ["Urls:Url2"] = mock2Url, })));
    using var client = new FlurlClient(app.CreateClient());

    //WHEN
    await client.Request("/broadcast").PostJsonAsync(new Work());

    //THEN
    var logEntries = mock1.LogEntries.Concat(mock2.LogEntries).OrderBy(entry => entry.RequestMessage.DateTime).ToList();
    logEntries[0].RequestMessage.AbsoluteUrl.Should().Be(mock1Url);
    logEntries[1].RequestMessage.AbsoluteUrl.Should().Be(mock2Url);
  }
}