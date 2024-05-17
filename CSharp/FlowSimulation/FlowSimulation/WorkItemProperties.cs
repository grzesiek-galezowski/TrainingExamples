using System.Collections.Immutable;

namespace FlowSimulation;

public record WorkItemProperties
{
    public int Points { get; init; } = 1;
    public int Priority { get; init; } = 0;
    public ImmutableList<string> Dependencies { get; set; } = [];
}