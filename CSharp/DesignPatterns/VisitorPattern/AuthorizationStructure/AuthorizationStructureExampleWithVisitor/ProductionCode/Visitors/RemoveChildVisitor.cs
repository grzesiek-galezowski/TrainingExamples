using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

public class RemoveChildVisitor(INode node) : INodeExternalVisitor
{
  public void Visit(Group group)
  {
    group.RemoveChild(node);
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