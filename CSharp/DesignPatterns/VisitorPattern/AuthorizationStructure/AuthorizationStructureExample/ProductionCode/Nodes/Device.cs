using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using LanguageExt;

namespace AuthorizationStructureExample.ProductionCode.Nodes;

public class Device(NodeId id, NodeId parentId, INode parent, Dictionary<string, string> properties) : INode
{
  public void Dump(IChangeEventsTarget target)
  {
    target.Added(id, parentId.Just());
  }

  public void AddChild(INode node)
  {
    throw new NotSupportedException("Devices do not have child nodes");
  }

  public LanguageExt.HashSet<NodeId> GetContainedDeviceIds()
  {
    return HashSet.createRange([id]);
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIds()
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

  public void CollectIdsForProperty(string propertyName, string expectedPropertyValue, System.Collections.Generic.HashSet<NodeId> collectionToFill)
  {
    if (properties.TryGetValue(propertyName, out var retrievedValue))
    {
      if (retrievedValue == expectedPropertyValue)
      {
        collectionToFill.Add(id);
      }
    }
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIdsThatAreIn(Seq<NodeId> searchedIds)
  {
    throw new NotSupportedException("Devices are not authorized for devices");
  }

  public LanguageExt.HashSet<NodeId> GetContainedDeviceIdsFromAmong(Seq<NodeId> searchedIds)
  {
    if(searchedIds.Contains(id))
    {
      return LanguageExt.HashSet<NodeId>.Empty.Add(id);
    }
    else
    {
      return LanguageExt.HashSet<NodeId>.Empty; 
    }
  }
}