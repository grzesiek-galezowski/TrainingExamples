using System.Collections.Immutable;
using Serilog.Core.Enrichers;
using ILogger = Serilog.ILogger;

namespace Application;

/// <summary>
/// Application logic composition root + API
/// </summary>
public class ApplicationLogic(IDeviceSubscriptionApi deviceSubscriptionApi, IDeviceApi deviceApi, ILogger logger)
{
  private readonly RoutingTable _routingTable = new();

  public IWeatherAppCommand CreateSubscribeCommand(
    SubscriptionRequestDto requestDto, ISubscribeCommandResponse response)
  {
    return new LoggingCommand(logger,
      new SubscribeCommand(requestDto.TenantId, requestDto.SubscriptionId, response, CreateDevicesQuery(requestDto),
        deviceSubscriptionApi, _routingTable),
      [
        new PropertyEnricher("SubscriptionId", requestDto.SubscriptionId),
        new PropertyEnricher("TenantId", requestDto.TenantId),
        new PropertyEnricher("ApplicationId", requestDto.ApplicationId),
        new PropertyEnricher("CommandName", "Subscribe")
      ]);
  }

  private IDeviceQuery CreateDevicesQuery(SubscriptionRequestDto requestDto)
  {
    if (requestDto is { QueryType: QueryTypes.NetworkParameters, NetworkParameters: not null })
    {
      return new QueryByNetworkParameters(deviceApi, new NetworkName(requestDto.NetworkParameters.NetworkName),
        new NetworkType(requestDto.NetworkParameters.NetworkType));
    }

    if(requestDto is { QueryType: QueryTypes.DeviceIds, DeviceIds: not null })
    {
      return new QueryByDeviceIds(requestDto.DeviceIds.Select(id => new DeviceId(id)).ToImmutableList());
    }

    if(requestDto is { QueryType: QueryTypes.GroupId, GroupId: not null })
    {
      return new QueryByGroupId(deviceApi, new GroupId(requestDto.GroupId));
    }

    // delaying because the query will be executed in logging scope
    return new InvalidQuery(requestDto.QueryType);
  }
}