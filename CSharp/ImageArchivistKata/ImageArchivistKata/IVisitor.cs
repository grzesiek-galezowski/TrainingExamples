namespace ImageArchivistKata
{
  public interface IVisitor
  {
    //not really needed for the task at hand
    void Visit(IUnidentifiedFile file);
    
    //not really needed for the task at hand
    void Visit(IFolder folder);

    void Visit(IImageFile image);
  }
}