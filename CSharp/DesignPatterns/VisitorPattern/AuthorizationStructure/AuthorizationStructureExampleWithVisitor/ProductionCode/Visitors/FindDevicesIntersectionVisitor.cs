using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;
using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class FindDevicesIntersectionVisitor(Seq<NodeId> searchedIds) : INodeVisitor
{
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
    Result = user.GetOwnedDeviceIdsThatAreIn(searchedIds);
  }

  public LanguageExt.HashSet<NodeId> Result { get; private set; }
}