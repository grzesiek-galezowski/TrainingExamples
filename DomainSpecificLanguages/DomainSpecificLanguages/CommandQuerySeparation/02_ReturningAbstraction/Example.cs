namespace CommandQuerySeparation._02_ReturningAbstraction
{
    public class Example
    {
      public void DoWhatever()
      {
        FileListDestination guiWidget = new FileListWidget();
        var fileSystem = new FileSystem(new DeletedFilesFactory());

        DoWork(fileSystem, guiWidget);
      }

      private static void DoWork(
        FileSystem fileSystem, 
        FileListDestination guiWidget)
      {
        var deletedFiles = fileSystem.DeleteFiles(@"C:\*.txt");
        deletedFiles.WriteOn(guiWidget);
      }
    }
}
