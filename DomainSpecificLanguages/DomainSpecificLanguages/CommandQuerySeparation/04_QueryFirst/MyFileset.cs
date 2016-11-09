using System.Collections.Generic;

namespace CommandQuerySeparation._04_QueryFirst
{
  public class MyFileset : Fileset
  {
    private readonly List<string> _deletedFilesList;

    public MyFileset(List<string> deletedFilesList)
    {
      _deletedFilesList = deletedFilesList;
    }

    public void WriteOn(FileListDestination destination)
    {
      
    }
  }
}