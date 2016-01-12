using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TddEbook.TddToolkit;

namespace PathValueObjectsRandori
{
    public class Class1
    {
      [Test]
      public void ShouldDOWHAT()
      {
        //GIVEN
        
        //THEN

        //THEN
      
      }
    }

/*
  // examples:
  // absolute file path: C:\lolek\lolek.txt
  // absolute directory path: C:\lolek\
  // file name: lolek.txt
  // file extension: txt
  // some features can be obtained using Path, FileInfo and DirectoryInfo classes

  public class AbsoluteFilePath //Example: C:\lolek\lolek.txt
  {

    private AbsoluteFilePath(string path) { }

    // must be not null
    // must be not empty
    // must be absolute

    public static AbsoluteFilePath Value(String path) { }
    public AbsoluteDirectoryPath ParentDirectory();
    public FileInfo AsFileInfo() { }

    public FileName FileName() { }

    public AbsoluteDirectoryPath Root() { }

    public override string ToString();
    public override bool Equals(Object obj);
    public override int GetHashCode();
  }


  public class AbsoluteDirectoryPath
  {
    private AbsoluteDirectoryPath(string path) { }

    //returns object when:
    //not empty
    //not null
    //is absolute
    public static AbsoluteDirectoryPath Value(string path);

    public string toString();
    public bool equals(object other);
    public int hashCode();

    public DirectoryInfo AsDirectoryInfo();

    public Maybe<AbsoluteDirectoryPath> ParentDirectory(); //nothing if path is root

    public AbsoluteDirectoryPath Root();
  }

  public class FileName
  {
    private FileName(string path);

    //not null
    //not empty
    //consists solely of file name (i.e. not "lol\lol.txt"
    public static FileName Value(String path);

    public override string ToString();
    public override bool Equals(object obj);
    public override int GetHashCode();

    public Maybe<FileExtension> Extension();

    public FileName ChangeExtensionTo(FileExtension value);
  }

  public class FileExtension
  {
    private FileExtension(string extension);
    
    //must not be null
    //must be empty
    //must consist solely of extension
    public static FileExtension Value(string extensionString);

    public override string ToString();
    public override bool Equals(Object obj);
    public override int GetHashCode();
  }
*/  
}
