using AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

public interface IGroup : INode
{
  void VisitChildren(INodeVisitor visitor);
  void AddChild(INode node);
  void RemoveChild(INode child);
  bool HasId(NodeId searchedNodeId);
}

public class Group(NodeId id, Maybe<NodeId> parentId, INode parent) : IGroup
{
  private readonly List<INode> _children = new();

  public void Accept(INodeVisitor visitor)
  {
    visitor.Visit(this);
  }

  public void VisitChildren(INodeVisitor visitor)
  {
    foreach (var child in _children)
    {
      child.Accept(visitor);
    }
  }

  public void Dump(IChangeEventsTarget target)
  {
    target.Added(id, parentId);
    foreach (var child in _children)
    {
      child.Dump(target);
    }
  }

  public void AddChild(INode node)
  {
    _children.Add(node);
  }

  public void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget)
  {
    nodesById.Remove(id);
    eventsTarget.Removed(id, parentId);
    foreach (var child in _children)
    {
      child.RemoveFrom(nodesById, eventsTarget);
    }
  }

  public void RemoveChild(INode child)
  {
    _children.Remove(child);
  }

  public void UnplugFromParent()
  {
    parent.Accept(new RemoveChildVisitor(this));
  }

  public bool HasId(NodeId searchedNodeId)
  {
    return id == searchedNodeId;
  }
}