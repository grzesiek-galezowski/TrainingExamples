using System.Collections.Immutable;
using Application;

namespace WeatherAppLogicSeparation;

public class DeviceSubscriptionHttpApi : IDeviceSubscriptionApi
{
  public async Task Subscribe(Guid subscriptionId, ImmutableList<DeviceId> deviceIds, CancellationToken token)
  {
    // fake
  }
}