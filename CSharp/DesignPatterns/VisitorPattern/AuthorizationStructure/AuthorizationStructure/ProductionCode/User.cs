namespace AuthorizationStructure.ProductionCode;

public class User(NodeId id, NodeId parentId) : INode
{
    public void Dump(IDumpTarget target)
    {
        target.Add(id, parentId.Just());
    }

    public void AddChild(INode node)
    {
        throw new NotImplementedException();
    }
}