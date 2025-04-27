namespace ImageArchivistKata
{
  public class UnidentifiedFileNode : IFileSystemNode, IUnidentifiedFile
  {
 
    public void Accept(IVisitor visitor)
    {
      visitor.Visit(this);
    }
  }
}