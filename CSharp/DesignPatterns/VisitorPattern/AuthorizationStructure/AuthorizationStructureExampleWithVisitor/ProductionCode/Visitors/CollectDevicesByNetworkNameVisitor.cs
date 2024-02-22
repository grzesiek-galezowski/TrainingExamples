using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class CollectDevicesByNetworkNameVisitor(string networkName) : INodeVisitor
{
  public readonly HashSet<NodeId> Result = new();

  public void Visit(Group group)
  {
    group.VisitChildren(this);
  }

  public void Visit(Device device)
  {
    device.CollectIdWhenNetworkNameIs(networkName, Result);
  }

  public void Visit(User user)
  {

  }
}