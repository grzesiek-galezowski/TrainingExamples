using System.Collections.Generic;

namespace CommandQuerySeparation._02_ReturningAbstraction
{
  public class FileSystem
  {
    private readonly DeletedFilesFactory _deletedFilesFactory;

    public FileSystem(DeletedFilesFactory deletedFilesFactory)
    {
      _deletedFilesFactory = deletedFilesFactory;
    }

    public DeletedFiles DeleteFiles(string blob)
    {
      var deletedFilesList = new List<string>();
      //bla bla bla
      return _deletedFilesFactory.CreateDeletedFilesFrom(deletedFilesList);
    }
  }
}