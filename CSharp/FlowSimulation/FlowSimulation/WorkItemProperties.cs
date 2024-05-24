using System.Collections.Immutable;
using Core.Maybe;

namespace FlowSimulation;

public record WorkItemProperties
{
  public int Points { get; init; } = 1;
  public ItemPriority Priority { get; init; } = ItemPriority.Normal;
  public ImmutableList<string> Dependencies { get; init; } = [];
  public Maybe<string> RequiredRole { get; init; }
}