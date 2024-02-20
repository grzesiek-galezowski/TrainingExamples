using System;
using System.Collections.Generic;

namespace AuthorizationStructureExample.ProductionCode;

public class User(NodeId id, NodeId parentId, INode parent) : INode
{
  public void Dump(IChangeEventsTarget target)
  {
    target.Added(id, parentId.Just());
  }

  public void AddChild(INode node)
  {
    throw new NotImplementedException();
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIds()
  {
    return LanguageExt.HashSet<NodeId>.Empty;
  }

  public LanguageExt.HashSet<NodeId> GetAuthorizedDeviceIds()
  {
    return parent.GetOwnedDeviceIds();
  }

  public bool Contains(NodeId searchedNodeId)
  {
    return id == searchedNodeId; //bug
  }

  public bool Owns(NodeId ownedId)
  {
    return parent.Contains(ownedId);
  }

  public void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget)
  {
    nodesById.Remove(id);
    eventsTarget.Removed(id, parentId.Just());
  }

  public void RemoveChild(INode child)
  {
    throw new NotImplementedException();
  }

  public void UnplugFromParent()
  {
    parent.RemoveChild(this);
  }
}