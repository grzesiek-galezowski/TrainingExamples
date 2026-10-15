namespace TrackClinic;

public record AppConfig
{
  public DatabaseInfoConfig DatabaseInfo { get; init; } = new();
  public LoggingConfig Logging { get; init; } = new();

  public static AppConfig From(ConfigurationManager builderConfiguration)
  {
    AppConfig appConfig = new();
    builderConfiguration.Bind(appConfig);
    appConfig.Assert();
    return appConfig;
  }

  private void Assert()
  {
    ArgumentNullException.ThrowIfNull(DatabaseInfo.ConnectionString);
  }
}