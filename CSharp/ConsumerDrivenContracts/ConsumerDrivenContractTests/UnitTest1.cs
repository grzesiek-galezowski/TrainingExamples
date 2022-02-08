using System;
using System.Collections.Generic;
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
                    .WithPath("/lol/lol2")
                    .UsingPost()
                    .WithParam("queryId", "12")
                    .WithBody(new ExactMatcher("Trolololo"))
                    .WithHeader(HeaderNames.Accept, MediaTypeNames.Application.Json)
                ).RespondWith(
                Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBodyAsJson(new { text = "hello" })
                    .WithHeader(HeaderNames.ContentType, MediaTypeNames.Application.Json)
                );

            var result = await proxyServer.Urls.First()
                .AppendPathSegment("lol/lol2")
                .SetQueryParam("queryId", "12")
                .WithHeader(HeaderNames.Accept, MediaTypeNames.Application.Json)
                .AllowAnyHttpStatus()
                .PostStringAsync("Trolololo");

            result.StatusCode.Should().Be(200);

            var response = await 
                MakeRequest("shouldReturnOk.json", wireMockServer.Urls.Single());

            response.StatusCode.Should().Be(200);

            wireMockServer.Stop();
            proxyServer.Stop();
        }

        private async Task<IFlurlResponse> MakeRequest(string mappingFile, string url)
        {
            var mapping = JsonConvert.DeserializeObject<Mapping>(
                await File.ReadAllTextAsync(mappingFile));

            var pathSegments = PathOf(mapping.Request);
            var httpMethod = MethodOf(mapping.Request);
            var headers = HeadersOf(mapping.Request);
            var queryParams = QueryParamsFrom(mapping.Request);
            var request = url
                .AppendPathSegments(pathSegments.Cast<object>())
                .WithHeaders(headers)
                .SetQueryParams(queryParams)
                .AllowAnyHttpStatus();

            IFlurlResponse response;
            if (httpMethod == HttpMethod.Get)
            {
                response = await request.GetAsync();
            }
            else if (httpMethod == HttpMethod.Post)
            {
                var body = BodyFrom(mapping.Request);
                response = await request.PostStringAsync(body);
            }
            else
            {
                throw new NotSupportedException(httpMethod.ToString());
            }

            return response;
        }

        private Dictionary<string, string> QueryParamsFrom(MapRequest mappingRequest)
        {
            return mappingRequest.Params.ToDictionary(p => p.Name, p => p.Matchers.Single().Pattern);
        }

        private string BodyFrom(MapRequest mappingRequest)
        {
            return mappingRequest.Body.Matcher.Pattern;
        }

        private Dictionary<string, string> HeadersOf(MapRequest mappingRequest)
        {
            return mappingRequest.Headers.ToDictionary(h => h.Name, h => h.Matchers.Single().Pattern);
        }

        private static string[] PathOf(MapRequest mappingRequest)
        {
            return mappingRequest.Path.Matchers.Select(m => m.Pattern).ToArray();
        }

        private HttpMethod MethodOf(MapRequest mappingRequest)
        {
            return mappingRequest.Methods.Single() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                _ => throw new NotSupportedException(mappingRequest.Methods.Single())
            };
        }
    }
}




public class Mapping
{
    public string Guid { get; set; }
    public string Title { get; set; }
    public MapRequest Request { get; set; }
    public MapResponse Response { get; set; }
}

public class MapRequest
{
    public MapPath Path { get; set; }
    public string[] Methods { get; set; }
    public MapHeader[] Headers { get; set; }
    public MapParam[] Params { get; set; }
    public MapBody Body { get; set; }
}

public class MapPath
{
    public Matcher[] Matchers { get; set; }
}

public class Matcher
{
    public string Name { get; set; }
    public string Pattern { get; set; }
    public bool IgnoreCase { get; set; }
}

public class MapBody
{
    public Matcher Matcher { get; set; }
}

public class MapHeader
{
    public string Name { get; set; }
    public Matcher[] Matchers { get; set; }
}

public class MapParam
{
    public string Name { get; set; }
    public Matcher[] Matchers { get; set; }
}

public class MapResponse
{
    public int StatusCode { get; set; }
    public MapBodyAsJson BodyAsJson { get; set; }
    public MapHeaders Headers { get; set; }
}

public class MapBodyAsJson
{
    public string text { get; set; }
}

public class MapHeaders
{
    public string ContentType { get; set; }
    public string Date { get; set; }
    public string Server { get; set; }
    public string TransferEncoding { get; set; }
}
