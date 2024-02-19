namespace AuthorizationStructure.ProductionCode;

public class AuthorizationStructure
{
    public static NodeId RootNodeId { get; } = new("Root", NodeType.Group);
    private readonly INode _rootNode;
    private readonly Dictionary<NodeId, INode> _nodesById;

    public AuthorizationStructure()
    {
        _rootNode = new Group(RootNodeId, Maybe<NodeId>.Nothing);
        _nodesById = new Dictionary<NodeId, INode>
    {
      {RootNodeId, _rootNode}
    };
    }

    public void AddDevice(NodeId parentId, string name)
    {
        var nodeId = NodeId.Device(name);
        var node = new Device(nodeId, parentId);
        Register(parentId, nodeId, node);
    }

    public void AddUser(NodeId parentId, string name) //BUG: lower and uppercase allowed in name
    {
        var nodeId = NodeId.User(name);
        var node = new User(nodeId, parentId);
        Register(parentId, nodeId, node);
    }

    public void AddGroup(NodeId parentId, string name)
    {
        var nodeId = NodeId.Group(name);
        var node = new Group(nodeId, parentId.Just());
        Register(parentId, nodeId, node);
    }

    public void Dump(IDumpTarget target) //BUG: also dumping a subtree (e.g. from a specific user)
    {
        _rootNode.Dump(target);
    }

    private void Register(NodeId parentId, NodeId nodeId, INode node)
    {
        _nodesById[nodeId] = node;
        _nodesById[parentId].AddChild(node);
    }
}