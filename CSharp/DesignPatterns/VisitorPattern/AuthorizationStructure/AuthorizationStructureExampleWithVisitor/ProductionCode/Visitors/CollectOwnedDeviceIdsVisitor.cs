using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class CollectOwnedDeviceIdsVisitor : INodeExternalVisitor
{
  public LanguageExt.HashSet<NodeId> Result { get; private set; } = LanguageExt.HashSet<NodeId>.Empty;

  public void Visit(Group group)
  {
    throw new NotSupportedException("Groups cannot own devices");
  }

  public void Visit(Device device)
  {
    throw new NotSupportedException("Devices cannot own devices");
  }

  public void Visit(User user)
  {
    Result = user.GetOwnedDeviceIds();
  }
}