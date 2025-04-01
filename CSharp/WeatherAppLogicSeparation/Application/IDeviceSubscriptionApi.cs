using System.Collections.Immutable;

namespace Application;

public interface IDeviceSubscriptionApi
{
  Task Subscribe(Guid subscriptionId, ImmutableList<DeviceId> deviceIds, CancellationToken token);
}