using System.Collections.Immutable;
using Application;
using Core.Maybe;

namespace WeatherAppLogicSeparation;

public class HttpDeviceApi : IDeviceApi
{
  public async Task<Maybe<ImmutableList<DeviceId>>> GetNetworkDeviceIds(
    NetworkName networkName, NetworkType networkType, CancellationToken token)
  {
    // fake 
    return ImmutableList<DeviceId>.Empty.Add(new DeviceId("1")).Just();
  }

  public async Task<Maybe<ImmutableList<DeviceId>>> GetGroupDeviceIds(GroupId groupId, CancellationToken token)
  {
    // fake
    return ImmutableList<DeviceId>.Empty.Add(new DeviceId("2")).Just();
  }
}