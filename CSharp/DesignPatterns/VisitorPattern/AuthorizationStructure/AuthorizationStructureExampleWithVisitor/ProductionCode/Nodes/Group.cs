using AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;
using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

public class Group(NodeId id, Maybe<NodeId> parentId, INode parent) : INode
{
  private readonly List<INode> _children = new();

  public void Accept(INodeExternalVisitor visitor)
  {
    visitor.Visit(this);
  }

  public void VisitChildren(INodeExternalVisitor visitor)
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

  public LanguageExt.HashSet<NodeId> GetContainedDeviceIds()
  {
    return HashSet.createRange(_children.SelectMany(c => c.GetContainedDeviceIds()));
  }

  public bool Contains(NodeId searchedNodeId)
  {
    return id == searchedNodeId || _children.Any(c => c.Contains(searchedNodeId));
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

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIdsFromAmong(Seq<NodeId> searchedIds)
  {
    return HashSet.createRange(_children.SelectMany(c => c.GetOwnedDeviceIdsFromAmong(searchedIds)));
  }
}