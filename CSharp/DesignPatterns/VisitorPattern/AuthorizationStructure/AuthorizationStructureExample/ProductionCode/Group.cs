using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace AuthorizationStructureExample.ProductionCode;

public class Group(NodeId id, Maybe<NodeId> parentId, INode parent) : INode
{
  private readonly List<INode> _children = new();

  public void Dump(IChangeEventsTarget target)
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

  public bool Contains(NodeId searchedNodeId)
  {
    return id == searchedNodeId || _children.Any(c => c.Contains(searchedNodeId));
  }

  public bool Owns(NodeId ownedId)
  {
    return false;
  }

  public void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget)
  {
    nodesById.Remove(id);
    eventsTarget.Removed(id, parentId);
    foreach (var child in _children)
    {
      child.RemoveFrom(nodesById, eventsTarget);
    }
  }

  public void RemoveChild(INode child)
  {
    _children.Remove(child);
  }

  public void UnplugFromParent()
  {
    parent.RemoveChild(this);
  }
}