using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl;
using Flurl.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using NUnit.Framework;
using WireMock.Admin.Mappings;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace ConsumerDrivenContractTests
{
    public class Tests
    {
        [Test]
        public async Task Test1()
        {
            var wireMockServer = WireMockServer.Start();
            var proxyServer = WireMockServer.Start(new WireMockServerSettings()
            {
                ProxyAndRecordSettings = new ProxyAndRecordSettings()
                {
                    SaveMapping = true,
                    SaveMappingToFile = true,
                    Url = wireMockServer.Urls.Single(),
                    ExcludedHeaders = new[] { "Host", "Date", "Server" }
                }
            });

            wireMockServer.Given(
                Request.Create()
                    .WithUrl(new ExactMatcher(wireMockServer.Urls.Single() + "/lol/lol2")).UsingGet()).RespondWith(
                Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBodyAsJson(new { text = "hello" })
                    .WithHeader(HeaderNames.ContentType, MediaTypeNames.Application.Json)
                );

            var result = await proxyServer.Urls.First()
                .AppendPathSegment("lol/lol2")
                .AllowAnyHttpStatus()
                .GetAsync();

            result.StatusCode.Should().Be(200);

            var mapping = JsonConvert.DeserializeObject<Mapping>(await File.ReadAllTextAsync("shouldReturnOk.json"));

            var pathSegments = PathOf(mapping.Request);
            var result2 = await wireMockServer.Urls.First()
                .AppendPathSegments(pathSegments)
                .AllowAnyHttpStatus()
                .SendAsync(MethodOf(mapping.Request));

            result2.StatusCode.Should().Be(200);

            wireMockServer.Stop();
            proxyServer.Stop();
        }

        private static string[] PathOf(MappingRequest mappingRequest)
        {
            return mappingRequest.Path.Matchers.Select(m => m.Pattern).ToArray();
        }

        private HttpMethod MethodOf(MappingRequest mappingRequest)
        {
            switch (mappingRequest.Methods.Single())
            {
                case "GET":
                    return HttpMethod.Get;
                case "POST":
                    return HttpMethod.Post;
                default:
                    throw new NotSupportedException(mappingRequest.Methods.Single());
            }
        }
    }
}


public class Mapping
{
    public string Guid { get; set; }
    public string Title { get; set; }
    public MappingRequest Request { get; set; }
    public MappingResponse Response { get; set; }
}

public class MappingRequest
{
    public MappingPath Path { get; set; }
    public string[] Methods { get; set; }
    public MappingHeader[] Headers { get; set; }
}

public class MappingPath
{
    public MappingMatcher[] Matchers { get; set; }
}

public class MappingMatcher
{
    public string Name { get; set; }
    public string Pattern { get; set; }
    public bool IgnoreCase { get; set; }
}

public class MappingHeader
{
    public string Name { get; set; }
    public MappingMatcher[] Matchers { get; set; }
}

public class MappingResponse
{
    public int StatusCode { get; set; }
    public MappingBodyAsJson BodyAsJson { get; set; }
    public MappingHeaders Headers { get; set; }
}

public class MappingBodyAsJson
{
    public string text { get; set; }
}

public class MappingHeaders
{
    public string ContentType { get; set; }
    public string Date { get; set; }
    public string Server { get; set; }
    public string TransferEncoding { get; set; }
}

