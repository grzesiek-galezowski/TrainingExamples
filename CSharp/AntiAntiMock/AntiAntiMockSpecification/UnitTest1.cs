using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using NUnit.Framework;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Util;

namespace AntiAntiMockSpecification
{
  public class Tests
  {
    [Test]
    public async Task Test1()
    {
      //GIVEN
      using var mock1 = WireMockServer.Start();
      using var mock2 = WireMockServer.Start();
      var mock1Url = mock1.Urls.Single() + "/";
      var mock2Url = mock2.Urls.Single() + "/";

      mock1.Given(Request.Create())
        .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
      mock2.Given(Request.Create())
        .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
      
      await using var host = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(
          builder =>
          {
            builder.ConfigureAppConfiguration((_, configurationBuilder) =>
            {
              configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>
              {
                ["Urls:Url1"] = mock1Url,
                ["Urls:Url2"] = mock2Url,
              });
            });
          });
      using var client = new FlurlClient(host.CreateClient());

      //WHEN
      await client.Request("/broadcast").PostJsonAsync(new WorkDto());

      //THEN
      var logEntries = mock1.LogEntries.Concat(mock2.LogEntries).OrderBy(entry => entry.RequestMessage.DateTime).ToList();
      logEntries[0].RequestMessage.AbsoluteUrl.Should().Be(mock1Url);
      logEntries[1].RequestMessage.AbsoluteUrl.Should().Be(mock2Url);
    }
  }
}