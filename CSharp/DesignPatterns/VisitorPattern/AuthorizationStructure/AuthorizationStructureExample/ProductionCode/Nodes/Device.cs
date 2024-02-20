using System;
using System.Collections.Generic;
using LanguageExt;

namespace AuthorizationStructureExample.ProductionCode.Nodes;

public class Device(NodeId id, NodeId parentId, INode parent) : INode
{
  public void Dump(IChangeEventsTarget target)
  {
    target.Added(id, parentId.Just());
  }

  public void AddChild(INode node)
  {
    throw new NotSupportedException("Devices do not have child nodes");
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIds()
  {
    return HashSet.createRange([id]);
  }

  public LanguageExt.HashSet<NodeId> GetAuthorizedDeviceIds()
  {
    throw new NotSupportedException("Devices are not authorized to use devices");
  }

  public bool Contains(NodeId searchedNodeId)
  {
    return id == searchedNodeId;
  }

  public bool Owns(NodeId ownedId)
  {
    throw new NotSupportedException("Devices cannot own anything");
  }

  public void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget)
  {
    nodesById.Remove(id);
    eventsTarget.Removed(id, parentId.Just());
  }

  public void RemoveChild(INode child)
  {
    throw new NotSupportedException("Devices do not have child nodes");
  }

  public void UnplugFromParent()
  {
    parent.RemoveChild(this);
  }
}

//BUG: not only remove operation, but also rename - does not cut off the subtree.