using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class CollectUserOwnedDeviceIdsVisitor : INodeVisitor
{
  public LanguageExt.HashSet<NodeId> Result { get; private set; } = LanguageExt.HashSet<NodeId>.Empty;

  public void Visit(IGroup group)
  {
    throw new NotSupportedException("Groups cannot own devices");
  }

  public void Visit(IDevice device)
  {
    throw new NotSupportedException("Devices cannot own devices");
  }

  public void Visit(IUser user)
  {
    Result = user.GetOwnedDeviceIds();
  }
}