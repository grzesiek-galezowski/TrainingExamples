using System.IO;
using AtmaFileSystem;

namespace ImageArchivistKata
{
  public class SystemIoBasedFileSystem : IFileSystem
  {
    public void Copy(AbsoluteFilePath source, AbsoluteFilePath destination)
    {
      File.Copy(source.ToString(), destination.ToString());
    }
  }
}