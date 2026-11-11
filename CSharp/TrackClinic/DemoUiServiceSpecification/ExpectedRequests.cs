namespace DemoUiServiceSpecification;

public static class ExpectedRequests
{
  public static string RetrieveSongDetailsRequest(string artist, string songName)
  {
    return $"/api/v1/artists/{artist}/songs/{songName}";
  }

  public static string CreateSongRequest(string artist)
  {
    return $"/api/v1/artists/{artist}/songs"; //bug move
    //bug add DTOs to matcher
  }
}