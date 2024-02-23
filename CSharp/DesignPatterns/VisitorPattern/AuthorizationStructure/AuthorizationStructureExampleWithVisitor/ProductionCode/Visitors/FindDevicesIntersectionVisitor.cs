using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;
using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class FindDevicesIntersectionVisitor(Seq<NodeId> searchedIds) : INodeExternalVisitor
{
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
    Result = user.GetOwnedDeviceIdsThatAreIn(searchedIds);
  }

  public LanguageExt.HashSet<NodeId> Result { get; private set; } = new();
}