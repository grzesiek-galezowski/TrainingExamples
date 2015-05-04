using AtmaFileSystem;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class Examples
  {
    [Fact]
    public void Example1_AssemblingAndDisassembling()
    {
      ////////////////////////
      // Disassembling:
      ////////////////////////
      
      PathWithFileName fullPath = PathWithFileName.To(@"C:\Program Files\Lolokimono\Config.txt");
      DirectoryPath directoryPath = fullPath.WithoutFileName();
      FileName fileName = fullPath.FileName();
      FileNameWithoutExtension fileNameWithoutExtension = fileName.WithoutExtension();
      Maybe<FileExtension> extension = fileName.Extension();
      DirectoryPath rootFromFullPath = fullPath.Root();
      DirectoryPath rootFromDirectoryPath = directoryPath.Root();

      ////////////////////////
      // Assembling:
      ////////////////////////

      
      PathWithFileName fullPathAssembled = directoryPath + fileNameWithoutExtension.With(extension.Value());

      //TODO
      // - add directory names
      // - add relative paths
      // - add WithoutRoot() to PathWithFileName method that returns relative directory
      // - add WithoutRoot() to PathToDirectory method that returns relative directory

    }

    [Fact]
    public void Example1_ParentDirectories()
    {
      PathWithFileName fullPath = PathWithFileName.To(@"C:\Program Files\Lolokimono\Config.txt");
      DirectoryPath directoryPath = fullPath.WithoutFileName();

      Maybe<DirectoryPath> parent = directoryPath.Parent();

      if (parent.Found)
      {
        Maybe<DirectoryPath> parentParent = parent.Value().Parent();
        if (parentParent.Found)
        {
          Maybe<DirectoryPath> parentParentParent = parentParent.Value().Parent();
        }
      }

     
    }

  }
}
