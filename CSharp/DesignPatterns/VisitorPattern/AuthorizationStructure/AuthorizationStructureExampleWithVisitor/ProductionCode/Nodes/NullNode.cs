namespace AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;

public class NullNode : INode
{
  public void Accept(INodeVisitor visitor)
  {
    throw new NotSupportedException();
  }

  public void Dump(IChangeEventsTarget target)
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