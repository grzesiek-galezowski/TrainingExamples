namespace AuthorizationStructureExampleWithVisitor.ProductionCode;

public interface INode
{
  void Accept(INodeVisitor visitor);
  void Dump(IChangeEventsTarget target);
  void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget);
  void UnplugFromParent();
}