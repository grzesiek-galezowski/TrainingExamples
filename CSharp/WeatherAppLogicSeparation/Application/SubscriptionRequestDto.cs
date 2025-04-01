using System.Collections.Immutable;

namespace Application;

public record SubscriptionRequestDto(
  Guid TenantId,
  Guid SubscriptionId,
  Guid ApplicationId,
  QueryTypes QueryType,
  NetworkParametersDto? NetworkParameters,
  ImmutableList<string>? DeviceIds,
  string? GroupId);