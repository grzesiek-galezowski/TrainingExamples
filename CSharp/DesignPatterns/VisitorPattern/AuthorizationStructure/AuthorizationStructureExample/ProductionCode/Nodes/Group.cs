using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace AuthorizationStructureExample.ProductionCode.Nodes;

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
    throw new NotSupportedException("Groups are not authorized to use devices");
  }

  public bool Contains(NodeId searchedNodeId)
  {
    return id == searchedNodeId || _children.Any(c => c.Contains(searchedNodeId));
  }

  public bool Owns(NodeId ownedId)
  {
    throw new NotSupportedException("Groups cannot own anything");
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

  public void CollectIdsForProperty(string propertyName, string expectedPropertyValue, System.Collections.Generic.HashSet<NodeId> collectionToFill)
  {
    foreach (var child in _children)
    {
      child.CollectIdsForProperty(propertyName, expectedPropertyValue, collectionToFill);
    }
  }
}