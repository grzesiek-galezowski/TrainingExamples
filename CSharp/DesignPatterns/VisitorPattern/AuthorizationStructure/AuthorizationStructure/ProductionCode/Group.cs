using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace AuthorizationStructure.ProductionCode;

public class Group(NodeId id, Maybe<NodeId> parentId, INode node) : INode
{
  private readonly List<INode> _children = new();

  public void Dump(IChangeEventTarget target)
  {
    target.Added(id, parentId);
    foreach (var child in _children)
    {
      child.Dump(target);
    }
  }

  public void AddChild(INode node)
  {
    _children.Add(node);
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIds()
  {
    return HashSet.createRange(_children.SelectMany(c => c.GetOwnedDeviceIds()));
  }

  public LanguageExt.HashSet<NodeId> GetAuthorizedDeviceIds()
  {
    throw new System.NotImplementedException();
  }
}