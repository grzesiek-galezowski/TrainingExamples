using System;
using AtmaFileSystem;

namespace ImageArchivistKata
{
  internal class ImageFile : IFileSystemNode, IImageFile
  {
    private readonly DateTime _dateTaken;
    private readonly AbsoluteFilePath _absolutePath;

    public ImageFile(AbsoluteFilePath absoluteFilePath, DateTime creationTime)
    {
      _dateTaken = creationTime;
      _absolutePath = absoluteFilePath;
    }

    public void Accept(IVisitor visitor)
    {
      visitor.Visit(this);
    }

    public void CopyToDateSubfolderIn(
      AbsoluteDirectoryPath absoluteDirectoryPath, 
      IFileSystem fileSystem, 
      string dateFormat)
    {
      //bug should duplicates be checked?
      var dateSubfolder = DirectoryName.Value(_dateTaken.ToString(dateFormat));
      var imageName = _absolutePath.FileName();
      fileSystem.Copy(_absolutePath, absoluteDirectoryPath + dateSubfolder + imageName);
    }
  }
}