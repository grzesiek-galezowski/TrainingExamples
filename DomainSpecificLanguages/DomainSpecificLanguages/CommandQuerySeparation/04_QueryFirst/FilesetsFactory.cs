using System.Collections.Generic;

namespace CommandQuerySeparation._04_QueryFirst
{
  public class FilesetsFactory
  {
    public Fileset CreateFileSetWith(List<string> deletedFilesList)
    {
      return new MyFileset(deletedFilesList);
    }
  }
}