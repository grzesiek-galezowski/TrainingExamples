using LanguageExt;

namespace AuthorizationStructure.ProductionCode;

public class NullNode : INode
{
  public void Dump(IChangeEventTarget target)
  {

  }

  public void AddChild(INode node)
  {

  }

  public LanguageExt.HashSet<NodeId> GetOwnedDeviceIds()
  {
    return LanguageExt.HashSet<NodeId>.Empty;
  }

  public HashSet<NodeId> GetAuthorizedDeviceIds()
  {
    throw new System.NotImplementedException();
  }
}