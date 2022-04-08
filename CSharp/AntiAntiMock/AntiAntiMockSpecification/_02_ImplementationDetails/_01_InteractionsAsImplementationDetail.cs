using FluentAssertions;
using NUnit.Framework;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;

namespace MockNoMockSpecification._02_ImplementationDetails;

//WARNING: THIS IS A ___TOY___ EXAMPLE (and probably bad design)
internal class _01_InteractionsAsImplementationDetail
{
  [Test]
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