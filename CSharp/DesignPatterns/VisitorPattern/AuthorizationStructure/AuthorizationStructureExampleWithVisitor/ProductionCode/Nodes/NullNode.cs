using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

public class NullNode : INode
{
  public void Accept(INodeExternalVisitor visitor)
  {
    throw new NotSupportedException();
  }

  public void Dump(IChangeEventsTarget target)
  {
    throw new NotSupportedException();
  }

  public LanguageExt.HashSet<NodeId> GetContainedDeviceIds()
  {
    throw new NotSupportedException();
  }

  public bool Contains(NodeId searchedNodeId)
  {
    throw new NotSupportedException();
  }

  public void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget)
  {
    throw new NotSupportedException();
  }

  public void UnplugFromParent()
  {
    throw new NotSupportedException();
  }
}