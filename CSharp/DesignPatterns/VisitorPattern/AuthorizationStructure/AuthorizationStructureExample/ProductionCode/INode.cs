using System.Collections.Generic;
using LanguageExt;

namespace AuthorizationStructureExample.ProductionCode;

public interface INode
{
  void Dump(IChangeEventsTarget target);
  void AddChild(INode node);
  LanguageExt.HashSet<NodeId> GetContainedDeviceIds();
  LanguageExt.HashSet<NodeId> GetOwnedDeviceIds();
  bool Contains(NodeId searchedNodeId);
  bool Owns(NodeId ownedId);
  void RemoveFrom(Dictionary<NodeId, INode> nodesById, IChangeEventsTarget eventsTarget);
  void RemoveChild(INode child);
  void UnplugFromParent();
  void CollectIdsForProperty(string propertyName, string expectedPropertyValue, System.Collections.Generic.HashSet<NodeId> collectionToFill);
  LanguageExt.HashSet<NodeId> GetOwnedDeviceIdsThatAreIn(Seq<NodeId> searchedIds);
  LanguageExt.HashSet<NodeId> GetContainedDeviceIdsFromAmong(Seq<NodeId> searchedIds);
}