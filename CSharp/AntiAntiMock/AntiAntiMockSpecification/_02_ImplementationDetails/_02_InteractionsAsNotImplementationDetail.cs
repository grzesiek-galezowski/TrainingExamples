using System.Collections.Concurrent;

namespace MockNoMockSpecification._02_ImplementationDetails;

//WARNING: THIS IS A ___TOY___ EXAMPLE (and probably bad design and a bad test)
internal class _02_InteractionsAsNotImplementationDetail
{
  [Test]
  public void ShouldSendWorkToRemoteSubscriberViaConnection()
  {
    //GIVEN
    var recipient = new PlugEnabledRemoteWorkRecipient();
    var work = Any.Instance<ManagedWork>();
    var connection = Substitute.For<IConnection>();

    //WHEN
    recipient.Assign(work, connection);

    //THEN
    Received.InOrder(() =>
    {
      connection.Open();
      connection.Send(work);
      connection.Close();
    });
  }

  [Test, Ignore("uncomment to see why plug-enabled object's interactions are not its implementation details")]
  public void BadPlugin()
  {
    //GIVEN
    var recipient = new PlugEnabledRemoteWorkRecipient();
    var work = Any.Instance<ManagedWork>();

    //WHEN
    recipient.Assign(work, new BadConnection()); //BadConnection doesn't adhere to the contract
  }
}

internal class BadConnection : IConnection
{
  public static readonly ConcurrentDictionary<string, List<ManagedWork>> Works = new();
  private readonly string _connectionString = Any.String();

  public void Open()
  {
  }

  public void Send(ManagedWork work)
  {
    Works[_connectionString].Add(work);
  }

  public void Close()
  {
    Works[_connectionString] = new List<ManagedWork>();
  }
}

public class PlugEnabledRemoteWorkRecipient
{
  public void Assign(ManagedWork work, IConnection connection)
  {
    //interactions with connection are __NOT__ implementation details!
    connection.Open();
    connection.Send(work);
    connection.Close();
  }
}

public interface IConnection
{
  void Open();
  void Send(ManagedWork work);
  void Close();
}