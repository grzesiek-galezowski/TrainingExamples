using System;
using System.Collections.Generic;
using System.Net;
using AuthorizationStructureExample.ProductionCode.Nodes;

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

  public LanguageExt.HashSet<NodeId> RetrieveIdsOfDevicesInNetwork(string networkName)
  {
    var collection = new HashSet<NodeId>();
    _nodesById[RootNodeId].CollectIdsForProperty(PropertyNames.NetworkName, networkName, collection);
    return LanguageExt.HashSet.createRange(collection);
  }

  public LanguageExt.HashSet<NodeId> RetrieveIdsOfDevicesInNetworkFromSubtree(string groupName, string networkName)
  {
    var collection = new HashSet<NodeId>();
    _nodesById[NodeId.Group(groupName)].CollectIdsForProperty(PropertyNames.NetworkName, networkName, collection);
    return LanguageExt.HashSet.createRange(collection);
  }

  public bool Contains(NodeId searchedNodeId, string groupName)
  {
    return _nodesById[NodeId.Group(groupName)].Contains(searchedNodeId);
  }

  public bool IsOwnershipBetween(string ownerId, NodeId ownedId)
  {
    return _nodesById[NodeId.User(ownerId)].Owns(ownedId);
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