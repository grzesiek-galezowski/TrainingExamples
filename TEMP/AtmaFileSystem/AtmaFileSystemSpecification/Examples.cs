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
      DirectoryPath directoryPath = fullPath.Directory();
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

    }
    [Fact]
    public void Example1_ParentDirectories()
    {
      PathWithFileName fullPath = PathWithFileName.To(@"C:\Program Files\Lolokimono\Config.txt");
      DirectoryPath directoryPath = fullPath.Directory();

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
