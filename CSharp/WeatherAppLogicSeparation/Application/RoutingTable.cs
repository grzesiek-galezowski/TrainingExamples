using System.Collections.Concurrent;

namespace Application;

public class RoutingTable
{
  // not thread-safe, bad idea to implement it like this in real world ^_^
  // but then again, this class doesn't serve any real purpose for now
  private ConcurrentDictionary<Guid, Guid> _routingTable = new();
  
  public void Add(Guid tenantId, Guid subscriptionId)
  {
    // also, some error handling should be here etc. Don't treat this class seriously ^_^
    _routingTable.TryAdd(tenantId, subscriptionId);
  }
}