using System.Collections.Immutable;
using Core.Maybe;

namespace Application;

internal class InvalidQuery(QueryTypes queryType) : IDeviceQuery
{
  public async Task<Maybe<ImmutableList<DeviceId>>> Resolve(CancellationToken token)
  {
    throw new BadResolutionException(queryType);
  }
}