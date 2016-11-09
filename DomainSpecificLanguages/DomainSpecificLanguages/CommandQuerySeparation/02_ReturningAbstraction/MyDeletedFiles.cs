using System.Collections.Generic;

namespace CommandQuerySeparation._02_ReturningAbstraction
{
  public class MyDeletedFiles : DeletedFiles
  {
    private readonly List<string> _deletedFilesList;

    public MyDeletedFiles(List<string> deletedFilesList)
    {
      _deletedFilesList = deletedFilesList;
    }

    public void WriteOn(FileListDestination destination)
    {
      
    }
  }
}