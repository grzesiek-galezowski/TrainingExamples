using System.Collections.Immutable;
using System.Net;
using Flurl;
using Flurl.Http;

namespace TrackClinicServiceSpecification.Automation.Zipkin;

public class ZipkinClient(string baseAddress) : IZipkinClient
{
  private readonly string _baseAddress = baseAddress.TrimEnd('/');

  public async Task<ImmutableList<ZipkinSpan>> GetTrace(string traceId, CancellationToken cancellationToken)
  {
    var response = await Request("api", "v2", "trace", traceId)
      .AllowHttpStatus("404")
      .GetAsync(cancellationToken: cancellationToken);

    if (response.StatusCode == (int)HttpStatusCode.NotFound)
    {
      return [];
    }

    return await response.GetJsonAsync<ImmutableList<ZipkinSpan>>() ?? [];
  }

  private Url Request(params object[] pathSegments) =>
    _baseAddress.AppendPathSegments(pathSegments);
}
