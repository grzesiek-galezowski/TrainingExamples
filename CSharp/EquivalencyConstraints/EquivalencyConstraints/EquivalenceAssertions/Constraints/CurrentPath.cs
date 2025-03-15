using System.Collections.Immutable;

namespace EquivalencyConstraints.EquivalenceAssertions.Constraints;

public record CurrentPath()
{
  public ImmutableList<string> Path { get; init; } = ImmutableList<string>.Empty;
}