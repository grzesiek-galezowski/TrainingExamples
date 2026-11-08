using System.Collections.Immutable;

namespace ServiceSpecificationBuildingBlocks.Zipkin;

public interface IZipkinClient
{
  Task<ImmutableList<ZipkinSpan>> GetTrace(string traceId, CancellationToken cancellationToken);
}
