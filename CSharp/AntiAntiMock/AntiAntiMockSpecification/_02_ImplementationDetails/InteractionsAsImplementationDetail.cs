using System.Collections.Concurrent;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;

namespace MockNoMockSpecification._02_ImplementationDetails;

internal class InteractionsAsImplementationDetail
{
  [Test]
  //WARNING: THIS IS A ___TOY___ EXAMPLE (and probably bad design)
  public void ShouldSendWorkToRemoteSubscriber()
  {
    //GIVEN
    var connectionString = Any.String();
    var recipient = new RemoteWorkRecipient(connectionString);
    var work = Any.Instance<ManagedWork>();

    //WHEN
    recipient.Assign(work);

    //THEN
    MyConnection.Works[connectionString].Should().Equal(work);
  }

  [Test]
  //WARNING: THIS IS A ___TOY___ EXAMPLE (and probably bad design and a bad test)
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
  //WARNING: THIS IS A ___TOY___ EXAMPLE (and probably bad design and a bad test)
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

public class RemoteWorkRecipient
{
  private readonly string _connectionString;

  public RemoteWorkRecipient(string connectionString)
  {
    //we are not mocking a string - more on this later...
    _connectionString = connectionString;
  }

  public void Assign(ManagedWork work)
  {
    //interactions with connection are implementation details!
    var connection = new MyConnection(_connectionString);
    connection.Open();
    connection.Send(work);
    connection.Close();
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

public record ManagedWork(string Id);