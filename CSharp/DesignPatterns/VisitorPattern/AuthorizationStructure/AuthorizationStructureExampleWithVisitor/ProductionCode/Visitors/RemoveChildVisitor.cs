using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class RemoveChildVisitor(INode node) : INodeVisitor
{
  public void Visit(IGroup group)
  {
    group.RemoveChild(node);
  }

  public void Visit(IDevice device)
  {
    throw new NotSupportedException("Devices do not have child nodes");
  }

  public void Visit(IUser user)
  {
    throw new NotSupportedException("Users do not have child nodes");
  }
}