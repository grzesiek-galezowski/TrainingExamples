using AtmaFileSystem;

namespace ImageArchivistKata
{
  public interface IFileSystem
  {
    void Copy(AbsoluteFilePath source, AbsoluteFilePath destination);
  }
}