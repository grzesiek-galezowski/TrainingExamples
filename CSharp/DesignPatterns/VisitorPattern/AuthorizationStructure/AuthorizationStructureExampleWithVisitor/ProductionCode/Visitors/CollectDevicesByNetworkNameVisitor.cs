using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class CollectDevicesByNetworkNameVisitor(string networkName) : INodeVisitor
{
  public readonly HashSet<NodeId> Result = new();

  public void Visit(IGroup group)
  {
    group.VisitChildren(this);
  }

  public void Visit(IDevice device)
  {
    device.CollectIdWhenNetworkNameIs(networkName, Result);
  }

  public void Visit(IUser user)
  {

  }
}