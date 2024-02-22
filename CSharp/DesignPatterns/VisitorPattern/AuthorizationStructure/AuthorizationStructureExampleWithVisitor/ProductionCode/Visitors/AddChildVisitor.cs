using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

internal class AddChildVisitor(INode childNode) : INodeVisitor
{
  public void Visit(Group group)
  {
    group.AddChild(childNode);
  }

  public void Visit(Device device)
  {
    throw new NotSupportedException("Devices do not have child nodes");
  }

  public void Visit(User user)
  {
    throw new NotSupportedException("Users do not have child nodes");
  }
}