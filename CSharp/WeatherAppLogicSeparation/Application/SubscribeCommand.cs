namespace Application;

public class SubscribeCommand(
  Guid tenantId,
  Guid subscriptionId,
  ISubscribeCommandResponse response,
  IDeviceQuery query,
  IDeviceSubscriptionApi deviceSubscriptionApi,
  RoutingTable routingTable) : IWeatherAppCommand
{
  public async Task Execute(CancellationToken token)
  {
    try
    {
      // note: not thread-safe ^_^
      var maybeDeviceIds = await query.Resolve(token);
      if (maybeDeviceIds.HasValue)
      {
        var deviceIds = maybeDeviceIds.Value();
        if (deviceIds.Any())
        {
          await deviceSubscriptionApi.Subscribe(subscriptionId, deviceIds, token);
          routingTable.Add(tenantId, subscriptionId);
          await response.SubscriptionCreated(subscriptionId);
        }
        else
        {
          await response.NoDevicesForQuery(subscriptionId);
        }
      }
      else
      {
        await response.ResolutionSubjectNotFound(subscriptionId);
      }
    }
    catch (SubscriptionAlreadyExistsException ex)
    {
      await response.SubscriptionAlreadyExists(subscriptionId, ex);
    }
    catch (BadResolutionException ex)
    {
      await response.ErrorWhileResolvingDevices(ex);
    }
    catch (Exception ex)
    {
      await response.UnexpectedError(ex);
    }
  }
}