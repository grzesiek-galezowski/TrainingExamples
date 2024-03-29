using AtmaFileSystem;

namespace Application;

public static class Tb03PatternFileName
{
  public static FileName For(int groupNumber, int patternNumber)
  {
    return FileName.Value($"TB03_PTN{groupNumber}_{patternNumber:D2}.PRM");
  }

  public static AbsoluteFilePath For(AbsoluteDirectoryPath path, int groupNumber, int patternNumber)
  {
    return path + For(groupNumber, patternNumber);
  }
}