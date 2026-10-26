using System.Collections.Immutable;

namespace TrackClinicServiceSpecification.Automation.Zipkin;

public interface IZipkinClient
{
  Task<ImmutableList<ZipkinSpan>> GetTrace(string traceId, CancellationToken cancellationToken);
}
