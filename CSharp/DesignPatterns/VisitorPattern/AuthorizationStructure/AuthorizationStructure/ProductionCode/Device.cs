namespace AuthorizationStructure.ProductionCode;

public class Device(NodeId nodeId, NodeId parentId) : INode
{
    public void Dump(IDumpTarget target)
    {
        target.Add(nodeId, parentId.Just());
    }

    public void AddChild(INode node)
    {
        throw new NotImplementedException();
    }
}