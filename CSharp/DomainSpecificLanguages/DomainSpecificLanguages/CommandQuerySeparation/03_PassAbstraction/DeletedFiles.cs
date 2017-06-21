namespace CommandQuerySeparation._03_PassAbstraction
{
  public interface DeletedFilesObserver
  {
    void Notify(string file);
  }
}