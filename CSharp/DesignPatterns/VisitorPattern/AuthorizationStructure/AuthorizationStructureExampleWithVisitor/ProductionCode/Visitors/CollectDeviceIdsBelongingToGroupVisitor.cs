using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;
using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class CollectDeviceIdsBelongingToGroupVisitor : INodeVisitor
{
  private readonly System.Collections.Generic.HashSet<NodeId> _result = new();

  public void Visit(IGroup group)
  {
    group.VisitChildren(this);
  }

  public void Visit(IDevice device)
  {
    device.CollectId(_result);
  }

  public void Visit(IUser user)
  {

  }

  public LanguageExt.HashSet<NodeId> Result => HashSet.createRange(_result);
}