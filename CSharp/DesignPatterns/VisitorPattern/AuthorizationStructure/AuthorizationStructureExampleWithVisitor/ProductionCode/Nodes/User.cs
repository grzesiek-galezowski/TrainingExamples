using AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;
using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

public class User(NodeId id, NodeId parentId, INode parent) : INode
{
  public void Accept(INodeVisitor visitor)
  {
    visitor.Visit(this);
  }

  public void Dump(IChangeEventsTarget target)
  {
    target.Added(id, parentId.Just());
  }

  public LanguageExt.HashSet<NodeId> GetContainedDeviceIds()
  {
    return LanguageExt.HashSet<NodeId>.Empty;
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIds()
  {
    return parent.GetContainedDeviceIds();
  }

  public bool Contains(NodeId searchedNodeId)
  {
    return id == searchedNodeId; //bug
  }

  public bool Owns(NodeId ownedId)
  {
    return parent.Contains(ownedId);
  }

  public void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget)
  {
    nodesById.Remove(id);
    eventsTarget.Removed(id, parentId.Just());
  }

  public void UnplugFromParent()
  {
    parent.Accept(new RemoveChildVisitor(this));
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIdsThatAreIn(Seq<NodeId> searchedIds)
  {
    return parent.GetOwnedDeviceIdsFromAmong(searchedIds);
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIdsFromAmong(Seq<NodeId> searchedIds)
  {
    return LanguageExt.HashSet<NodeId>.Empty;
  }
}