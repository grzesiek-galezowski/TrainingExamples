using System;
using NSubstitute;
using PloehKata;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
  public class ConnectionCommandSpecification
  {
    [Fact]
    public void ShouldReadBothUsersAndAttemptConnectionBetweenThemThenUpdateConector()
    {
      //GIVEN
      var connectionInProgress = Any.Instance<IConnectionInProgress>();
      var user1Id = Any.String();
      var user2Id = Any.String();
      var repository = Substitute.For<IUserRepository>();
      var connector = Substitute.For<IConnector>();
      var connectee = Any.Instance<IConnectee>();
      var command = new ConnectionCommand(connectionInProgress, user1Id, user2Id, repository);

      repository.LookupConnector(user1Id).Returns(connector);
      repository.LookupConnectee(user2Id).Returns(connectee);

      //WHEN
      command.Execute();

      //THEN
      Received.InOrder(() =>
      {
        connector.AttemptConnectionWith(connectee, connectionInProgress);
        connector.WriteTo(repository);
      });
    }

    //bug invalid ID -> exception?
  }
}