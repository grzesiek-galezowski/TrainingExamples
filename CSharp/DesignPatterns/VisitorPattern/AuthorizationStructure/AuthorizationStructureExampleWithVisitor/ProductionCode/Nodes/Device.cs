using AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;
using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

public interface IDevice
{
  void CollectIdWhenNetworkNameIs(string networkName, System.Collections.Generic.HashSet<NodeId> collectionToFill);
  void CollectIdWhenItIsAmong(Seq<NodeId> searchedIds, System.Collections.Generic.HashSet<NodeId> result);
  void CollectId(System.Collections.Generic.HashSet<NodeId> result);
  bool HasId(NodeId searchedNodeId);
}

public class Device(NodeId id, NodeId parentId, INode parent, Dictionary<string, string> properties) : INode, IDevice
{
  public void Accept(INodeVisitor visitor)
  {
    visitor.Visit(this);
  }

  public void Dump(IChangeEventsTarget target)
  {
    target.Added(id, parentId.Just());
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

  public void CollectIdWhenNetworkNameIs(string networkName, System.Collections.Generic.HashSet<NodeId> collectionToFill)
  {
    if (properties[PropertyNames.NetworkName] == networkName)
    {
      collectionToFill.Add(id);
    }
  }

  public void CollectIdWhenItIsAmong(Seq<NodeId> searchedIds, System.Collections.Generic.HashSet<NodeId> result)
  {
    if(searchedIds.Contains(id))
    {
      result.Add(id);
    }
  }

  public void CollectId(System.Collections.Generic.HashSet<NodeId> result)
  {
    result.Add(id);
  }

  public bool HasId(NodeId searchedNodeId)
  {
    return id == searchedNodeId;
  }
}