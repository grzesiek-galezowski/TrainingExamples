using AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;
using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

public interface IUser : INode
{
  LanguageExt.HashSet<NodeId> GetOwnedDeviceIds();
  bool Owns(NodeId ownedId);
  LanguageExt.HashSet<NodeId> GetOwnedDeviceIdsThatAreIn(Seq<NodeId> searchedIds);
  bool HasId(NodeId searchedNodeId);
}

public class User(NodeId id, NodeId parentId, INode parent) : IUser
{
  public void Accept(INodeVisitor visitor)
  {
    visitor.Visit(this);
  }

  public void Dump(IChangeEventsTarget target)
  {
    target.Added(id, parentId.Just());
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIds()
  {
    var visitor = new CollectDeviceIdsBelongingToGroupVisitor();
    parent.Accept(visitor);
    return visitor.Result;
  }

  public bool Owns(NodeId ownedId)
  {
    var visitor = new ContainsNodeIdVisitor(ownedId);
    parent.Accept(visitor);
    return visitor.Result;
  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIdsThatAreIn(Seq<NodeId> searchedIds)
  {
    var visitor = new GetOwnedDeviceIdsFromAmongVisitor(searchedIds);
    parent.Accept(visitor);
    return visitor.Result;
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

  public bool HasId(NodeId searchedNodeId)
  {
    return id == searchedNodeId;
  }
}