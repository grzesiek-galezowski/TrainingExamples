namespace EquivalencyConstraintsSpecification.E2E.Fixture;

public record Company
{
  public string name;
  public required Person Director { get; init; }
}