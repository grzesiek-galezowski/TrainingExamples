using System.Collections.Immutable;
using Core.Maybe;

namespace FlowSimulation.ProductionCode;

public record WorkItemProperties
{
  public WorkItemProperties()
  {
  }

  public WorkItemProperties(string requiredRole, ImmutableList<ItemId> dependencies)
  {
    RequiredRole = requiredRole;
    Dependencies = dependencies;
  }

  public WorkItemProperties(int priority, ImmutableList<ItemId> dependencies) : this()
  {
    Priority = priority;
    Dependencies = dependencies;
  }

  public WorkItemProperties(string requiredRole) : this()
  {
    RequiredRole = requiredRole;
  }

  public WorkItemProperties(int priority) : this()
  {
    Priority = priority;
  }

  public WorkItemProperties(string requiredRole, int points) : this()
  {
    RequiredRole = requiredRole;
    Points = points;
  }

  public WorkItemProperties(ImmutableList<ItemId> dependencies) : this()
  {
    Dependencies = dependencies;
  }

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