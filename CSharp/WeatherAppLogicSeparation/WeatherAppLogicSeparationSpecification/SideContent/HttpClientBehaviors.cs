using System.Net;

namespace WeatherAppLogicSeparationSpecification.SideContent;

public class HttpClientBehaviors
{
  [Test]
  public async Task ShouldAllowDoingNonHttpActivities()
  {
    //GIVEN
    string? extractedBody = null;
    using var httpClient = new HttpClient(new HandlerWithLambda(body => extractedBody = body));

    //WHEN
    var response = await httpClient.PostAsync("http://localhost", new StringContent("some content"));

    //THEN
    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    Assert.That(extractedBody, Is.EqualTo("some content"));
  }
}

file class HandlerWithLambda(Action<string> action) : HttpMessageHandler
{
  protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
  {
    action(await request.Content!.ReadAsStringAsync(cancellationToken));
    return new HttpResponseMessage(HttpStatusCode.OK);
  }
}