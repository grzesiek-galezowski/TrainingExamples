namespace CommandQuerySeparation._03_PassAbstraction
{
    public class Example
    {
      public void DoWhatever()
      {
        FileListWidget guiWidget1 = new FileListWidget();
        FileListWidget guiWidget2 = new FileListWidget();
        var fileSystem = new FileSystem();

        DoWork(fileSystem, guiWidget1);
        DoWork(fileSystem, guiWidget2);
      }

      private static void DoWork(
        FileSystem fileSystem, 
        DeletedFilesObserver guiWidget)
      {
        fileSystem.DeleteFiles(@"C:\*.txt", guiWidget);
      }
    }
}
