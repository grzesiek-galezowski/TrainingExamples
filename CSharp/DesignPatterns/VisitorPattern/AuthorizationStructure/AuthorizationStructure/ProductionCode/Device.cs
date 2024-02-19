using System;
using LanguageExt;

namespace AuthorizationStructure.ProductionCode;

public class Device(NodeId nodeId, NodeId parentId, INode node) : INode
{
  public void Dump(IChangeEventTarget target)
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