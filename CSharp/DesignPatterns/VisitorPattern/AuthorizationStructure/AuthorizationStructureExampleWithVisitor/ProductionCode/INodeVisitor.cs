using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode;

public interface INodeVisitor
{
  void Visit(Group group);
  void Visit(Device device);
  void Visit(User user);
}