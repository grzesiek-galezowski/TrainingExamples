namespace AuthorizationStructureExample.ProductionCode;

//BUG: special root group class?

public interface IChangeEventsTarget
{
  void Added(NodeId nodeId, Maybe<NodeId> parentId);
}

//BUG: dump, add user, add device, add group, send to all devices from ID, listen to notifications, filter??, check authorization
//BUG: potentially
//BUG: node removals
//BUG: final removals for devices
//BUG: produce notifications on change to authorization structure