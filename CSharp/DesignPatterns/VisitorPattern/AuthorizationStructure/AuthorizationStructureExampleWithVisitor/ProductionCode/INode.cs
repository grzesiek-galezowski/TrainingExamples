namespace AuthorizationStructureExampleWithVisitor.ProductionCode;

public interface INode
{
  void Accept(INodeExternalVisitor visitor);
  //void Accept(INodeHierarchyVisitor visitor); //bug
  void Dump(IChangeEventsTarget target);
  LanguageExt.HashSet<NodeId> GetContainedDeviceIds();
  bool Contains(NodeId searchedNodeId);
  void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget);
  void UnplugFromParent();
}