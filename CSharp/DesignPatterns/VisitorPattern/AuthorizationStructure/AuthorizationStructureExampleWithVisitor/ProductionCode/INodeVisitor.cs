using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode;

public interface INodeVisitor
{
  void Visit(IGroup group);
  void Visit(IDevice device);
  void Visit(IUser user);
}