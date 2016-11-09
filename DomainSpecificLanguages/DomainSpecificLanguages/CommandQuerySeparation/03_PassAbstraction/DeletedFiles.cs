namespace CommandQuerySeparation._03_PassAbstraction
{
  public interface DeletedFilesObserver
  {
    void Add(string file);
  }
}