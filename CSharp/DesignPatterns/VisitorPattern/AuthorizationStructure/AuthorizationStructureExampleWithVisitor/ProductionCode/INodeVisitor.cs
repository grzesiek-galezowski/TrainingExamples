using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode;

public interface INodeExternalVisitor
{
  void Visit(Group group);
  void Visit(Device device);
  void Visit(User user);
}

public interface INodeInternalVisitor
{
  void Visit(Group group);
  void Visit(Device device);
  void Visit(User user);
}

public interface INodeHierarchyVisitor : INodeExternalVisitor, INodeInternalVisitor;