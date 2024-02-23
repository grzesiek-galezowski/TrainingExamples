using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;
using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class GetOwnedDeviceIdsFromAmongVisitor(Seq<NodeId> searchedIds) : INodeExternalVisitor //bug
{
  private readonly System.Collections.Generic.HashSet<NodeId> _result = new();

  public LanguageExt.HashSet<NodeId> Result => HashSet.createRange(_result);

  public void Visit(Group group)
  {
    group.VisitChildren(this);
  }

  public void Visit(Device device)
  {
    device.CollectIdWhenItIsAmong(searchedIds, _result);
  }

  public void Visit(User user)
  {
  }
}