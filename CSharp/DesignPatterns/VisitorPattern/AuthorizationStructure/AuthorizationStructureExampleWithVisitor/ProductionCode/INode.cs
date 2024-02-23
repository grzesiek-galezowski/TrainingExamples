namespace AuthorizationStructureExampleWithVisitor.ProductionCode;

public interface INode
{
  void Accept(INodeVisitor visitor);
  //void Accept(INodeHierarchyVisitor visitor); //bug
  void Dump(IChangeEventsTarget target);
  void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget);
  void UnplugFromParent();
}