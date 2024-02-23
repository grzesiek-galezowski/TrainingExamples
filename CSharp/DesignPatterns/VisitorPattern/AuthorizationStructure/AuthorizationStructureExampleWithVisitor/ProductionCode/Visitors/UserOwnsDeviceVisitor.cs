using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class UserOwnsDeviceVisitor(NodeId ownedNodeId) : INodeVisitor
{
  public void Visit(IGroup group)
  {
    throw new NotSupportedException("Groups cannot own anything");
  }

  public void Visit(IDevice device)
  {
    throw new NotSupportedException("Devices cannot own anything");
  }

  public void Visit(IUser user)
  {
    Result = user.Owns(ownedNodeId);
  }

  public bool Result { get; private set; }
}