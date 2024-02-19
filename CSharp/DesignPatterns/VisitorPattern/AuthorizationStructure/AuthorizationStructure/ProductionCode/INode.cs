namespace AuthorizationStructure.ProductionCode;

public interface INode
{
  void Dump(IChangeEventTarget target);
  void AddChild(INode node); //BUG: candidate for visitor
  LanguageExt.HashSet<NodeId> GetOwnedDeviceIds();
  LanguageExt.HashSet<NodeId> GetAuthorizedDeviceIds();
}