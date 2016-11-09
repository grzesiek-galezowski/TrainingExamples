using System.Collections.Generic;

namespace CommandQuerySeparation._01_ReturningPlainTypes
{
    public class Example
    {
      public void DoWhatever()
      {
        FileListDestination guiWidget = new FileListWidget();
        var fileSystem = new FileSystem();

        DoWork(fileSystem, guiWidget, new DeletedFilesFactory());
      }

      private static void DoWork(
        FileSystem fileSystem, 
        FileListDestination guiWidget, 
        DeletedFilesFactory deletedFilesFactory)
      {
        /*!!!!!!!!*/
        List<string> deletedFilesList = fileSystem.DeleteFiles(@"C:\*.txt");

        //bla bla bla
        var deletedFiles = deletedFilesFactory.CreateDeletedFilesFrom(deletedFilesList);
        deletedFiles.WriteOn(guiWidget);
      }
    }
}
