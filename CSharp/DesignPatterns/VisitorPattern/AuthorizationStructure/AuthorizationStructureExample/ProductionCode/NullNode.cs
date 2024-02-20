using LanguageExt;

namespace AuthorizationStructureExample.ProductionCode;

public class NullNode : INode
{
  public void Dump(IChangeEventsTarget target)
  {

  }

  public void AddChild(INode node)
  {

  }

  public HashSet<NodeId> GetOwnedDeviceIds()
  {
    return HashSet<NodeId>.Empty;
  }

  public HashSet<NodeId> GetAuthorizedDeviceIds()
  {
    throw new System.NotImplementedException();
  }
}