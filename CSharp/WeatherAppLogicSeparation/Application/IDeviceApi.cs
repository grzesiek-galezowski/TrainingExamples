using System.Collections.Immutable;
using Core.Maybe;

namespace Application;

public interface IDeviceApi
{
  Task<Maybe<ImmutableList<DeviceId>>> GetNetworkDeviceIds(
    NetworkName networkName, NetworkType networkType, CancellationToken token);
  Task<Maybe<ImmutableList<DeviceId>>> GetGroupDeviceIds(GroupId groupId, CancellationToken token);
}