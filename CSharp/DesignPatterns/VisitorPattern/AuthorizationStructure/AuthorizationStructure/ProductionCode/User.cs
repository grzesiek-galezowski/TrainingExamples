using System;
using LanguageExt;

namespace AuthorizationStructure.ProductionCode;

public class User(NodeId id, NodeId parentId, INode parent) : INode
{
  public void Dump(IDumpTarget target)
  {
    target.Add(id, parentId.Just());
  }

  public void AddChild(INode node)
  {
    throw new NotImplementedException();
  }

  public HashSet<NodeId> GetOwnedDeviceIds()
  {
    return HashSet<NodeId>.Empty;
  }

  public HashSet<NodeId> GetAuthorizedDeviceIds()
  {
    return parent.GetOwnedDeviceIds();
  }
}