using System;
using System.Collections.Generic;
using LanguageExt;

namespace AuthorizationStructureExample.ProductionCode.Nodes;

public class NullNode : INode
{
  public void Dump(IChangeEventsTarget target)
  {
    throw new NotSupportedException();
  }

  public void AddChild(INode node)
  {
    throw new NotSupportedException();
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIds()
  {
    throw new NotSupportedException();
  }

  public LanguageExt.HashSet<NodeId> GetAuthorizedDeviceIds()
  {
    throw new NotSupportedException();
  }

  public bool Contains(NodeId searchedNodeId)
  {
    throw new NotSupportedException();
  }

  public bool Owns(NodeId ownedId)
  {
    throw new NotSupportedException();
  }

  public void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget)
  {
    throw new NotSupportedException();
  }

  public void RemoveChild(INode child)
  {
    throw new NotSupportedException();
  }

  public void UnplugFromParent()
  {
    throw new NotSupportedException();
  }

  public void CollectIdsForProperty(string propertyName, string expectedPropertyValue, System.Collections.Generic.HashSet<NodeId> collectionToFill)
  {
    throw new NotSupportedException();
  }

  public LanguageExt.HashSet<NodeId> GetAuthorizedDeviceIdsThatAreIn(Seq<NodeId> searchedIds)
  {
    throw new NotSupportedException();
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIdsFromAmong(Seq<NodeId> searchedIds)
  {
    throw new NotSupportedException();
  }
}