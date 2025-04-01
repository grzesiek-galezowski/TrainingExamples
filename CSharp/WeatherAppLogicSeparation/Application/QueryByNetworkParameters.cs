using System.Collections.Immutable;
using Core.Maybe;

namespace Application;

public class QueryByNetworkParameters(IDeviceApi deviceApi, NetworkName networkName, NetworkType networkType) : IDeviceQuery
{
  public async Task<Maybe<ImmutableList<DeviceId>>> Resolve(CancellationToken token)
  {
    return await deviceApi.GetNetworkDeviceIds(networkName, networkType, token);
  }
}