using System.Collections.Immutable;
using Core.Maybe;

namespace Application;

public interface IDeviceQuery
{
  Task<Maybe<ImmutableList<DeviceId>>> Resolve(CancellationToken token);
}