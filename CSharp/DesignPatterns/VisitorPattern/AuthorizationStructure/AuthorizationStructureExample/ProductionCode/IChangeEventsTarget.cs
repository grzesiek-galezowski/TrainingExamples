namespace AuthorizationStructureExample.ProductionCode;

public interface IChangeEventsTarget
{
  void Added(NodeId nodeId, Maybe<NodeId> parentId);
  void Removed(NodeId nodeId, Maybe<NodeId> parent);
}

//BUG: Send to all devices from ID
//BUG: final removals for devices
//BUG: renames
//BUG: retrieve intersection of ids from passed collecti