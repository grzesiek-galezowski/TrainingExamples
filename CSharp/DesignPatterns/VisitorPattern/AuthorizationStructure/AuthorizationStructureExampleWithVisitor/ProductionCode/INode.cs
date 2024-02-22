using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode;

public interface INode
{
  void Accept(INodeVisitor visitor);
  void Dump(IChangeEventsTarget target);
  LanguageExt.HashSet<NodeId> GetContainedDeviceIds();
  bool Contains(NodeId searchedNodeId);
  void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget);
  void UnplugFromParent();
  LanguageExt.HashSet<NodeId> GetOwnedDeviceIdsFromAmong(Seq<NodeId> searchedIds);
}