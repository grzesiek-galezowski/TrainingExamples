using System.IO;
using AtmaFileSystem;

namespace ImageArchivistKata
{
  public class Program
  {
    public static void Main2(string[] args)
    {
      var sourceRoot = new DirectoryInfo("MyImages");

      var composite = FileSystemFactory.CreateCompositeFrom("MyImages");

      var destinationRoot = AbsoluteDirectoryPath.Value("C:\\TEMP");
      composite.Accept(new ImageCopyingVisitor(destinationRoot, new SystemIoBasedFileSystem()));

    }
  }
}