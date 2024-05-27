using System.Collections.Immutable;
using Core.Maybe;

namespace FlowSimulation;

public record WorkItemProperties
{
  public int Points { get; init; } = 1;
  public int Priority { get; init; } = 0;
  public ImmutableList<ItemId> Dependencies { get; init; } = [];

#pragma warning disable S2376 // Write-only properties should not be used
  public string RequiredRole
#pragma warning restore S2376 // Write-only properties should not be used
  {
    init => MaybeRequiredRole = ((RoleId)value).Just();
  }

  public Maybe<RoleId> MaybeRequiredRole
  {
    private init;
    get;
  }
}