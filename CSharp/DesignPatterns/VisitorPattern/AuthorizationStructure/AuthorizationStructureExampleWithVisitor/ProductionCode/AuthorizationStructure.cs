using AuthorizationStructureExampleWithVisitor.ProductionCode.Nodes;
using AuthorizationStructureExampleWithVisitor.ProductionCode.Visitors;
using Core.Either;
using LanguageExt;

namespace AuthorizationStructureExampleWithVisitor.ProductionCode;

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

  public void AddDevice(string parentGroupName, string name, string networkName)
  {
    try
    {
      var nodeId = NodeId.Device(name);
      var parentId = NodeId.Group(parentGroupName);
      var node = new Device(nodeId, parentId, _nodesById[parentId], new Dictionary<string, string>()
      {
        [PropertyNames.NetworkName] = networkName
      });
      Register(parentId, nodeId, node);
    }
    catch (KeyNotFoundException e)
    {
      throw new InvalidOperationException("parent id not found", e);
    }
  }

  public void AddUser(string parentGroupName, string name)
  {
    try
    {
      var nodeId = NodeId.User(name);
      var parentId = NodeId.Group(parentGroupName);
      var node = new User(nodeId, parentId, _nodesById[parentId]);
      Register(parentId, nodeId, node);
    }
    catch (KeyNotFoundException e)
    {
      throw new InvalidOperationException("parent id not found", e);
    }
  }

  public void AddGroup(string parentGroupName, string name)
  {
    try
    {
      var nodeId = NodeId.Group(name);
      var parentId = NodeId.Group(parentGroupName);
      var node = new Group(nodeId, parentId.Just(), _nodesById[parentId]);
      Register(parentId, nodeId, node);
    }
    catch (KeyNotFoundException e)
    {
      throw new InvalidOperationException("parent id not found", e);
    }
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
    if (!_nodesById.TryAdd(nodeId, node))
    {
      throw new InvalidOperationException($"{nodeId} already exists");
    }
    _nodesById[parentId].Accept(new AddChildVisitor(node));
    _eventsTarget.Added(nodeId, parentId.Just());
  }

  public LanguageExt.HashSet<NodeId> RetrieveIdsOfDevicesOwnedByUser(string userName)
  {
    var collectOwnedDeviceIdsVisitor = new CollectOwnedDeviceIdsVisitor();
    _nodesById[NodeId.User(userName)].Accept(collectOwnedDeviceIdsVisitor);
    return collectOwnedDeviceIdsVisitor.Result;
  }

  public LanguageExt.HashSet<NodeId> RetrieveIdsOfDevicesBelongingToGroup(string name)
  {
    return _nodesById[NodeId.Group(name)].GetContainedDeviceIds();
  }

  public LanguageExt.HashSet<NodeId> RetrieveIdsOfDevicesInNetwork(string networkName)
  {
    return RetrieveIdsOfDevicesInNetworkFromSubtree(RootNodeId.Name, networkName);
  }

  public LanguageExt.HashSet<NodeId> RetrieveIdsOfDevicesInNetworkFromSubtree(string groupName, string networkName)
  {
    var visitor = new CollectDevicesByNetworkNameVisitor(networkName);
    _nodesById[NodeId.Group(groupName)].Accept(visitor);
    return HashSet.createRange(visitor.Result);
  }

  public LanguageExt.HashSet<NodeId> RetrieveIdsOfDevicesOwnedUserFromAmong(Seq<string> searchedIds, string user)
  {
    var findDevicesIntersectionVisitor = new FindDevicesIntersectionVisitor(searchedIds.Select(NodeId.Device));
    _nodesById[NodeId.User(user)].Accept(findDevicesIntersectionVisitor);
    return findDevicesIntersectionVisitor.Result;
  }

  public bool Contains(NodeId searchedNodeId, string groupName)
  {
    return _nodesById[NodeId.Group(groupName)].Contains(searchedNodeId);
  }

  public bool IsOwnershipBetween(string ownerId, string ownedDeviceId) //BUG: fix the ownership terminology
  {
    var nodeVisitor = new UserOwnsDeviceVisitor(NodeId.Device(ownedDeviceId));
    _nodesById[NodeId.User(ownerId)].Accept(nodeVisitor);
    return nodeVisitor.Result;
  }

  public void Remove(NodeId nodeId)
  {
    if (nodeId == RootNodeId)
    {
      throw new InvalidOperationException("Root node cannot be removed");
    }
    _nodesById[nodeId].UnplugFromParent();
    _nodesById[nodeId].RemoveFrom(_nodesById, _eventsTarget);
  }

  public bool Exists(NodeId nodeId)
  {
    return _nodesById.ContainsKey(nodeId);
  }
}