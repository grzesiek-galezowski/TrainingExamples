using System.Collections.Immutable;
using Core.Maybe;

namespace Application;

public class QueryByDeviceIds(ImmutableList<DeviceId> deviceIds) : IDeviceQuery
{
  public async Task<Maybe<ImmutableList<DeviceId>>> Resolve(CancellationToken token)
  {
    return deviceIds.Just();
  }
}