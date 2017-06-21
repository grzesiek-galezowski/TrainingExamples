using System.Collections.Generic;

namespace CommandQuerySeparation._03_PassAbstraction
{
  public class FileSystem
  {

    public void DeleteFiles(string blob, DeletedFilesObserver operationObserver)
    {
      var deletedFilesList = new List<string>();
      
      //bla bla bla
      foreach (var fileName in deletedFilesList)
      {
        operationObserver.Notify(fileName);
      }
    }
  }
}