using System.Collections.Generic;

namespace CommandQuerySeparation._02_ReturningAbstraction
{
  public class DeletedFilesFactory
  {
    public DeletedFiles CreateDeletedFilesFrom(List<string> deletedFilesList)
    {
      return new MyDeletedFiles(deletedFilesList);
    }
  }
}