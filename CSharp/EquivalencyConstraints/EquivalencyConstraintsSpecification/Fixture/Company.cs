namespace EquivalencyConstraintsSpecification.Fixture;

public record Company
{
  public required Person Director { get; init; }
}