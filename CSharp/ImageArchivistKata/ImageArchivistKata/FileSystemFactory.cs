using System;
using System.Collections.Generic;
using System.IO;
using AtmaFileSystem;

namespace ImageArchivistKata
{
  public static class FileSystemFactory
  {
    public static IFileSystemNode CreateCompositeFrom(string absoluteDirPath)
    {
      var directory = new DirectoryInfo(absoluteDirPath);
      var children = new List<IFileSystemNode>();
      foreach (var fileSystemInfo in directory.EnumerateFileSystemInfos())
      {
        switch (fileSystemInfo)
        {
          case DirectoryInfo di:
            children.Add(CreateCompositeFrom(di.FullName));
            break;
          case FileInfo fi:
            if (fi.Extension.Equals("bmp", StringComparison.InvariantCultureIgnoreCase))
            {
              children.Add(new ImageFile(AbsoluteFilePath.Value(fi.FullName), fi.CreationTimeUtc));
            }
            else
            {
              children.Add(new UnidentifiedFileNode());
            }

            break;
        }
      }

      return new FolderNode(children);
    }
  }
}