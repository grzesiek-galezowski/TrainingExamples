namespace ImageArchivistKata
{
  public interface IFileSystemNode
  {
    void Accept(IVisitor visitor);
  }
}