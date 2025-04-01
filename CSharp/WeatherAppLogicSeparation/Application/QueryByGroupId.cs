using System.Collections.Immutable;
using Core.Maybe;

namespace Application;

public class QueryByGroupId(IDeviceApi deviceApi, GroupId groupId) : IDeviceQuery
{
  public async Task<Maybe<ImmutableList<DeviceId>>> Resolve(CancellationToken token)
  {
    return await deviceApi.GetGroupDeviceIds(groupId, token);
  }
}