using System.Collections.Immutable;
using Core.Maybe;

namespace FlowSimulation;

public record WorkItemProperties
{
  public int Points { get; init; } = 1;
  public int Priority { get; init; } = 0;
  public ImmutableList<ItemId> Dependencies { get; init; } = [];
  public Maybe<string> RequiredRole { get; init; }
}