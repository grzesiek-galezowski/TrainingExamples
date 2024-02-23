using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class ContainsNodeIdVisitor(NodeId searchedNodeId) : INodeVisitor
{
  public void Visit(IGroup group)
  {
    if (group.HasId(searchedNodeId))
    {
      Result = true;
    }
    else
    {
      group.VisitChildren(this);
    }
  }

  public void Visit(IDevice device)
  {
    if (device.HasId(searchedNodeId))
    {
      Result = true;
    }
  }

  public void Visit(IUser user)
  {
    if (user.HasId(searchedNodeId))
    {
      Result = true;
    }
  }

  public bool Result { get; private set; }
}