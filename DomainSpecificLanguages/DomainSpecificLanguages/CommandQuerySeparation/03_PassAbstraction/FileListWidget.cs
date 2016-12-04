namespace CommandQuerySeparation._03_PassAbstraction
{
  public class FileListWidget : DeletedFilesObserver
  {
    public void Notify(string file)
    {
      throw new System.NotImplementedException();
    }
  }
}