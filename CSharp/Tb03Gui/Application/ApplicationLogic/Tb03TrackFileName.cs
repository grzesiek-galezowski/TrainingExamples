using AtmaFileSystem;

namespace Application.ApplicationLogic;

public static class Tb03TrackFileName
{
  public static AbsoluteFilePath For(AbsoluteDirectoryPath absoluteDirectoryPath, int trackNumber)
  {
    return absoluteDirectoryPath + For(trackNumber);
  }

  public static FileName For(int trackNumber)
  {
    return FileName.Value($"TB03_TRACK{trackNumber}.PRM");
  }
}