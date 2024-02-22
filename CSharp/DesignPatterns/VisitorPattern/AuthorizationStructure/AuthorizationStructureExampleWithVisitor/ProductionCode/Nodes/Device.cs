using AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;
using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

public class Device(NodeId id, NodeId parentId, INode parent, Dictionary<string, string> properties) : INode
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
    return HashSet.createRange([id]);
  }

  public bool Contains(NodeId searchedNodeId)
  {
    return id == searchedNodeId;
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

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIdsFromAmong(Seq<NodeId> searchedIds)
  {
    if(searchedIds.Contains(id))
    {
      return LanguageExt.HashSet<NodeId>.Empty.Add(id);
    }
    else
    {
      return LanguageExt.HashSet<NodeId>.Empty; 
    }
  }

  public void CollectIdWhenNetworkNameIs(string networkName, System.Collections.Generic.HashSet<NodeId> collectionToFill)
  {
    if (properties[PropertyNames.NetworkName] == networkName)
    {
      collectionToFill.Add(id);
    }
  }
}