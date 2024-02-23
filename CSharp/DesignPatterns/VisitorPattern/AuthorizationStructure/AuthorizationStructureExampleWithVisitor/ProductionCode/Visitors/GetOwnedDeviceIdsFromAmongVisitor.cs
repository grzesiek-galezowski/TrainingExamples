using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;
using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class GetOwnedDeviceIdsFromAmongVisitor(Seq<NodeId> searchedIds) : INodeVisitor //bug
{
  private readonly System.Collections.Generic.HashSet<NodeId> _result = new();

  public LanguageExt.HashSet<NodeId> Result => HashSet.createRange(_result);

  public void Visit(IGroup group)
  {
    group.VisitChildren(this);
  }

  public void Visit(IDevice device)
  {
    device.CollectIdWhenItIsAmong(searchedIds, _result);
  }

  public void Visit(IUser user)
  {
  }
}