using System.Collections.Generic;

namespace CommandQuerySeparation._01_ReturningPlainTypes
{
  public class DeletedFilesFactory
  {
    public DeletedFiles CreateDeletedFilesFrom(List<string> deletedFilesList)
    {
      return new MyDeletedFiles(deletedFilesList);
    }
  }
}