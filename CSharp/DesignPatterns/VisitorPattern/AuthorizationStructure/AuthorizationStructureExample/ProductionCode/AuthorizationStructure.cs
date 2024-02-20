using System.Collections.Generic;

namespace AuthorizationStructureExample.ProductionCode;

public class AuthorizationStructure
{
  public static NodeId RootNodeId { get; } = new("Root", NodeType.Group);
  private readonly INode _rootNode;
  private readonly Dictionary<NodeId, INode> _nodesById;
  private readonly IChangeEventsTarget _eventsTarget;

  public AuthorizationStructure(IChangeEventsTarget authorizationStructureEventsTarget)
  {
    _eventsTarget = authorizationStructureEventsTarget;
    _rootNode = new Group(RootNodeId, Maybe<NodeId>.Nothing, new NullNode());
    _nodesById = new Dictionary<NodeId, INode>
    {
      { RootNodeId, _rootNode }
    };
  }

  public void AddDevice(NodeId parentId, string name)
  {
    var nodeId = NodeId.Device(name);
    var node = new Device(nodeId, parentId, _nodesById[parentId]);
    Register(parentId, nodeId, node);
  }

  public void AddUser(NodeId parentId, string name) //BUG: lower and uppercase allowed in name
  {
    var nodeId = NodeId.User(name);
    var node = new User(nodeId, parentId, _nodesById[parentId]);
    Register(parentId, nodeId, node);
  }

  public void AddGroup(NodeId parentId, string name)
  {
    var nodeId = NodeId.Group(name);
    var node = new Group(nodeId, parentId.Just(), _nodesById[parentId]);
    Register(parentId, nodeId, node);
  }

  public void Dump()
  {
    _rootNode.Dump(_eventsTarget);
  }

  public void DumpStartingFrom(NodeId subtreeRoot)
  {
    _nodesById[subtreeRoot].Dump(_eventsTarget);
  }

  private void Register(NodeId parentId, NodeId nodeId, INode node)
  {
    _nodesById[nodeId] = node;
    _nodesById[parentId].AddChild(node);
    _eventsTarget.Added(nodeId, parentId.Just());
  }

  public LanguageExt.HashSet<NodeId> RetrieveIdsOfDevicesOwnedByUser(string userName)
  {
    return _nodesById[NodeId.User(userName)].GetAuthorizedDeviceIds();
  }

  public LanguageExt.HashSet<NodeId> RetrieveIdsOfDevicesBelongingToGroup(string name)
  {
    return _nodesById[NodeId.Group(name)].GetOwnedDeviceIds();
  }
}