using System;
using System.Collections.Generic;
using LanguageExt;

namespace AuthorizationStructureExample.ProductionCode.Nodes;

public class User(NodeId id, NodeId parentId, INode parent) : INode
{
  public void Dump(IChangeEventsTarget target)
  {
    target.Added(id, parentId.Just());
  }

  public void AddChild(INode node)
  {
    throw new NotSupportedException("Users do not have child nodes");
  }

  public LanguageExt.HashSet<NodeId> GetContainedDeviceIds()
  {
    return LanguageExt.HashSet<NodeId>.Empty;
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIds()
  {
    return parent.GetContainedDeviceIds();
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
    throw new NotSupportedException("Devices do not have child nodes");
  }

  public void UnplugFromParent()
  {
    parent.RemoveChild(this);
  }

  public void CollectIdsForProperty(string propertyName, string expectedPropertyValue, System.Collections.Generic.HashSet<NodeId> collectionToFill)
  {
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIdsThatAreIn(Seq<NodeId> searchedIds)
  {
    return parent.GetContainedDeviceIdsFromAmong(searchedIds);
  }

  public LanguageExt.HashSet<NodeId> GetContainedDeviceIdsFromAmong(Seq<NodeId> searchedIds)
  {
    return LanguageExt.HashSet<NodeId>.Empty;
  }
}