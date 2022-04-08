using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MockNoMockSpecification._02_ImplementationDetails;

public class MyConnection
{
  public static readonly ConcurrentDictionary<string, List<ManagedWork>> Works = new();
  private readonly string _connectionString;

  public MyConnection(string connectionString)
  {
    _connectionString = connectionString;
  }

  public void Open()
  {
    Works[_connectionString] = new List<ManagedWork>();
  }

  public void Send(ManagedWork work)
  {
    Works[_connectionString].Add(work);
  }

  public void Close()
  {
  }
}