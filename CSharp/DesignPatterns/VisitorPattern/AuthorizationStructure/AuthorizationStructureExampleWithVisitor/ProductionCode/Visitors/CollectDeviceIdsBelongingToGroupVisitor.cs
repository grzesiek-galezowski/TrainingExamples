using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;
using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class CollectDeviceIdsBelongingToGroupVisitor : INodeExternalVisitor
{
  private readonly System.Collections.Generic.HashSet<NodeId> _result = new();

  public void Visit(Group group)
  {
    group.VisitChildren(this);
  }

  public void Visit(Device device)
  {
    device.CollectId(_result);
  }

  public void Visit(User user)
  {

  }

  public LanguageExt.HashSet<NodeId> Result => HashSet.createRange(_result);
}