using System.Collections.Generic;

namespace ImageArchivistKata
{
  internal class FolderNode : IFileSystemNode, IFolder
  {
    private readonly List<IFileSystemNode> _children;

    public FolderNode(List<IFileSystemNode> children)
    {
      _children = children;
    }

    public void Accept(IVisitor visitor)
    {
      visitor.Visit(this);
      foreach (var fileSystemNode in _children)
      {
        fileSystemNode.Accept(visitor);
      }
    }
  }
}