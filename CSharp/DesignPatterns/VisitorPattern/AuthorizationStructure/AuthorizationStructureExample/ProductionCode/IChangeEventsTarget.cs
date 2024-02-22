namespace AuthorizationStructureExample.ProductionCode;

public interface IChangeEventsTarget
{
  void Added(NodeId nodeId, Maybe<NodeId> parentId);
  void Removed(NodeId nodeId, Maybe<NodeId> parent);
}