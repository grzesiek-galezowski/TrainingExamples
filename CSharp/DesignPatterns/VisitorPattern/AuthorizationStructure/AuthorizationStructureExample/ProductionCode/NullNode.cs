using System.Collections.Generic;

namespace AuthorizationStructureExample.ProductionCode;

public class NullNode : INode
{
  public void Dump(IChangeEventsTarget target)
  {

  }

  public void AddChild(INode node)
  {

  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIds()
  {
    return LanguageExt.HashSet<NodeId>.Empty;
  }

  public LanguageExt.HashSet<NodeId> GetAuthorizedDeviceIds()
  {
    throw new System.NotImplementedException();
  }

  public bool Contains(NodeId searchedNodeId)
  {
    throw new System.NotImplementedException();
  }

  public bool Owns(NodeId ownedId)
  {
    throw new System.NotImplementedException();
  }

  public void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget)
  {
    throw new System.NotImplementedException();
  }

  public void RemoveChild(INode child)
  {
    throw new System.NotImplementedException();
  }

  public void UnplugFromParent()
  {
    throw new System.NotImplementedException();
  }
}