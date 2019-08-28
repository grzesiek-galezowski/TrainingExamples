using System;
using System.Collections.Generic;
using AtmaFileSystem;
using NSubstitute;
using TddXt.AnyRoot.Time;
using TddXt.XNSubstitute.Root;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace ImageArchivistKata
{
  public class Tests
  {
    [Fact]
    public void ShouldBeAbleToCopyFilesToTheirRespectiveFolders()
    {
      var filePath1 = Any.Instance<AbsoluteFilePath>();
      var filePath2 = Any.Instance<AbsoluteFilePath>();
      var filePath3 = Any.Instance<AbsoluteFilePath>();
      var filePath4 = Any.Instance<AbsoluteFilePath>();
      var creationTime1 = Any.DateTime();
      var creationTime2 = Any.DateTime();
      var creationTime3 = Any.DateTime();
      var creationTime4 = Any.DateTime();
      var destinationRoot = Any.Instance<AbsoluteDirectoryPath>();
      var fileSystem = Substitute.For<IFileSystem>();

      var root = new FolderNode(new List<IFileSystemNode>()
      {
        new FolderNode(
          new List<IFileSystemNode>
          {
            new UnidentifiedFileNode(),
            new UnidentifiedFileNode(),
            new ImageFile(filePath1, creationTime1),
            new ImageFile(filePath2, creationTime2),
          }),
        new FolderNode(
          new List<IFileSystemNode>
          {
            new UnidentifiedFileNode(),
            new UnidentifiedFileNode(),
            new ImageFile(filePath3, creationTime3),
            new ImageFile(filePath4, creationTime4),
          }),
      });

      root.Accept(new ImageCopyingVisitor(destinationRoot, fileSystem));

      XReceived.Only(() =>
      {
        fileSystem.Copy(filePath1, 
          PathConsistingOf(destinationRoot, creationTime1, filePath1.FileName()));
        fileSystem.Copy(filePath2, 
          PathConsistingOf(destinationRoot, creationTime2, filePath2.FileName()));
        fileSystem.Copy(filePath3, 
          PathConsistingOf(destinationRoot, creationTime3, filePath3.FileName()));
        fileSystem.Copy(filePath4, 
          PathConsistingOf(destinationRoot, creationTime4, filePath4.FileName()));
      });
    }

    private static AbsoluteFilePath PathConsistingOf(AbsoluteDirectoryPath destinationRoot, DateTime creationTime1, FileName fileName)
    {
      return destinationRoot + DirectoryName.Value(creationTime1.ToString("yy-MM")) + fileName;
    }
  }
}
