namespace CommandQuerySeparation._04_QueryFirst
{
    public class Example
    {
      public void DoWhatever()
      {
        FileListDestination guiWidget = new FileListWidget();
        var fileSystem = new FileSystem(new FilesetsFactory());

        DoWork(fileSystem, guiWidget);
      }

      private static void DoWork(
        FileSystem fileSystem, 
        FileListDestination guiWidget)
      {
        var fileset = fileSystem.GetFilesetFor(@"C:\*.txt");
        fileset.Delete(); //how do we implement this?
        fileset.WriteOn(guiWidget);
      }
    }
}
