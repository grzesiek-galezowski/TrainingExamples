using AtmaFileSystem;
using ImageArchivistKata;

public interface IImageFile
{
  void CopyToDateSubfolderIn(AbsoluteDirectoryPath absoluteDirectoryPath, IFileSystem fileSystem, string dateFormat);
}