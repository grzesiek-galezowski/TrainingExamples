using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl;
using Flurl.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        [Test, Ignore("fails ^_^")]
        public async Task Test1()
        {
            var wireMockServer = WireMockServer.Start();
            var proxyServer = WireMockServer.Start(new WireMockServerSettings
            {
                ProxyAndRecordSettings = new ProxyAndRecordSettings
                {
                    SaveMapping = true,
                    SaveMappingToFile = true,
                    Url = wireMockServer.Urls.Single(),
                    ExcludedHeaders = new[] { "Host", "Date", "Server", "Accept-Encoding" }
                }
            });

            wireMockServer.Given(
                Request.Create()
                    .WithPath("/lol/lol2")
                    .UsingPost()
                    .WithParam("queryId", "12")
                    .WithBody(new JsonMatcher(new { key = "value"}))
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
                .PostJsonAsync(new { key = "value"});

            result.StatusCode.Should().Be(200);

            var mappingModel = JsonConvert.DeserializeObject<MappingModel>(
                await File.ReadAllTextAsync("shouldReturnOk.json"));
            var response = await MakeRequest(wireMockServer.Urls.Single(), mappingModel);


            AssertResponse(response, mappingModel);

            wireMockServer.Stop();
            proxyServer.Stop();
        }

        private static void AssertResponse(IFlurlResponse response, MappingModel mappingModel)
        {
            response.StatusCode.Should().Be(int.Parse(mappingModel.Response.StatusCode.ToString()));
            response.Headers.ToDictionary(h => h.Name, h => h.Value).Should().Contain(mappingModel.Response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()));
        }

        private async Task<IFlurlResponse> MakeRequest(string url, MappingModel mapping)
        {
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
                response = await Post(request, mapping.Request.Body);
            }
            else
            {
                throw new NotSupportedException(httpMethod.ToString());
            }

            return response;
        }

        private static async Task<IFlurlResponse> Post(IFlurlRequest request, BodyModel requestBody)
        {
            IFlurlResponse response;
            if (requestBody.Matcher.Name == "StringMatcher")
            {
                var body = requestBody.Matcher.Pattern.ToString();
                response = await request.PostStringAsync(body);
            }
            else if (requestBody.Matcher.Name == "JsonMatcher")
            {
                var body = ((JObject)requestBody.Matcher.Pattern).ToObject<Dictionary<string, object>>();
                response = await request.PostJsonAsync(body);
            }
            else
            {
                throw new NotSupportedException();
            }

            return response;
        }

        private Dictionary<string, string> QueryParamsFrom(RequestModel mappingRequest)
        {
            return mappingRequest.Params.ToDictionary(p => p.Name, p => p.Matchers.Single().Pattern.ToString());
        }

        private Dictionary<string, string> HeadersOf(RequestModel mappingRequest)
        {
            return mappingRequest.Headers.ToDictionary(h => h.Name, h => h.Matchers.Single().Pattern.ToString());
        }

        private static string[] PathOf(RequestModel mappingRequest)
        {
            var obj = (JObject)mappingRequest.Path;
            var pathModel = obj.ToObject<PathModel>();
            return pathModel.Matchers.Select(m => m.Pattern.ToString()).ToArray();
        }

        private HttpMethod MethodOf(RequestModel mappingRequest)
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
