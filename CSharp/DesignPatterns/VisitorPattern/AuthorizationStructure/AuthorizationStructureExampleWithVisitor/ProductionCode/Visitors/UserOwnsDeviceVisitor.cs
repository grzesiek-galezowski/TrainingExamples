using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class UserOwnsDeviceVisitor(NodeId ownedNodeId) : INodeExternalVisitor
{
  public void Visit(Group group)
  {
    throw new NotSupportedException("Groups cannot own anything");
  }

  public void Visit(Device device)
  {
    throw new NotSupportedException("Devices cannot own anything");
  }

  public void Visit(User user)
  {
    Result = user.Owns(ownedNodeId);
  }

  public bool Result { get; private set; }
}