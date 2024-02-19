namespace AuthorizationStructure.ProductionCode;

public class Group(NodeId id, Maybe<NodeId> parentId) : INode
{
    private readonly List<INode> _children = new();

    public void Dump(IDumpTarget target)
    {
        target.Add(id, parentId);
        foreach (var child in _children)
        {
            child.Dump(target);
        }
    }

    public void AddChild(INode node)
    {
        _children.Add(node);
    }
}