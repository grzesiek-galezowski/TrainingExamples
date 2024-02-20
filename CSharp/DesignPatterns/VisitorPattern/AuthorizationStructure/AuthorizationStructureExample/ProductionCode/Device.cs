using System;
using LanguageExt;

namespace AuthorizationStructureExample.ProductionCode;

public class Device(NodeId nodeId, NodeId parentId, INode node) : INode
{
  public void Dump(IChangeEventsTarget target)
  {
    target.Added(nodeId, parentId.Just());
  }

  public void AddChild(INode node)
  {
    throw new NotImplementedException(); //BUG:
  }

  public HashSet<NodeId> GetOwnedDeviceIds()
  {
    return HashSet.createRange([nodeId]);
  }

  public HashSet<NodeId> GetAuthorizedDeviceIds()
  {
    throw new NotImplementedException();
  }
}

//BUG: not only remove operation, but also rename - does not cut off the subtree.