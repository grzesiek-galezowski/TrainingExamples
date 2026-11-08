namespace DemoUi.Adapters.Configuration;

public record KeycloakConfig
{
  public required Uri Authority { get; init; }
  public required Uri MetadataAddress { get; init; }

  public void Validate()
  {
    ArgumentNullException.ThrowIfNull(Authority);
    ArgumentNullException.ThrowIfNull(MetadataAddress);
  }
}
