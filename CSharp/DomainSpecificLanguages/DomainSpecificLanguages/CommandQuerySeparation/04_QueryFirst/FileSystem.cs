using System.Collections.Generic;

namespace CommandQuerySeparation._04_QueryFirst
{
  public class FileSystem
  {
    private readonly FilesetsFactory _fileSetsFactory;

    public FileSystem(FilesetsFactory fileSetsFactory)
    {
      _fileSetsFactory = fileSetsFactory;
    }

    public Fileset GetFilesetFor(string blob)
    {
      var files = new List<string>();

      //bla bla bla fill the list

      return _fileSetsFactory.CreateFileSetWith(files);
    }
  }
}