using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

internal class AddChildVisitor(INode childNode) : INodeVisitor
{
  public void Visit(IGroup group)
  {
    group.AddChild(childNode);
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