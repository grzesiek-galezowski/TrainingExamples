namespace Lib;

public static class SecretStore
{
  public static async Task<string> ReadSpeechServiceSubscriptionKey() 
    => await ReadSecret("subscriptionKey");
  public static async Task<string> ReadSpeechServiceRegion() 
    => await ReadSecret("serviceRegion");
  public static async Task<string> ReadLuisKey() 
    => await ReadSecret("luis");
  public static async Task<string> ReadLuisUrl() 
    => await ReadSecret("luisAppUrl");
  public static async Task<string> ReadLuisApp() 
    => await ReadSecret("luisApp");

  private static async Task<string> ReadSecret(string secretName)
  {
    return await File.ReadAllTextAsync(
      $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\Documents\\__KEYS\\{secretName}.txt");
  }
}