using System.Collections.Generic;

namespace AuthorizationStructureExample.ProductionCode;

public interface INode
{
  void Dump(IChangeEventsTarget target);
  void AddChild(INode node); //BUG: candidate for visitor
  LanguageExt.HashSet<NodeId> GetOwnedDeviceIds();
  LanguageExt.HashSet<NodeId> GetAuthorizedDeviceIds();
  bool Contains(NodeId searchedNodeId);
  bool Owns(NodeId ownedId);
  void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget);
  void RemoveChild(INode child);
  void UnplugFromParent();
}