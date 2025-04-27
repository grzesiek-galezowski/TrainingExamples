using AtmaFileSystem;

namespace ImageArchivistKata
{
  public class ImageCopyingVisitor : IVisitor
  {
    private readonly AbsoluteDirectoryPath _destinationRoot;
    private readonly IFileSystem _fileSystem;

    public ImageCopyingVisitor(AbsoluteDirectoryPath destinationRoot, IFileSystem fileSystem)
    {
      _destinationRoot = destinationRoot;
      _fileSystem = fileSystem;
    }

    public void Visit(IUnidentifiedFile file)
    {
      
    }

    public void Visit(IFolder folder)
    {
      
    }

    public void Visit(IImageFile image)
    {
      //TODO should duplicates be handled, or not?
      image.CopyToDateSubfolderIn(_destinationRoot, _fileSystem, "yy-MM");
    }
  }
}