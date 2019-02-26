using FluentAssertions;
using NSubstitute;
using NSubstitute.Core.Arguments;
using PloehKata;
using TddXt.AnyRoot.Strings;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
    public class ConnectorSpecification //bug no connector
  {
    [Fact]
    public void ShouldMakeConnecteeConnectFromItself()
    {
      //GIVEN
      var connector = new Connector(Any.Instance<UserDto>());
      var connectee = Substitute.For<IConnectee>();
      var connectionInProgress = Substitute.For<IConnectionInProgress>();

      //WHEN
      connector.AttemptConnectionWith(connectee, connectionInProgress);

      //THEN
      connectee.Received(1).AttemptConnectionFrom((IExistingConnector)connector, connectionInProgress);
    }

    [Fact]
    public void ShouldAddConnecteeIdToItsConnectionsWhenConnectedWithThisId()
    {
      //GIVEN
      var connecteeId = Any.String();
      var userDto = Any.Instance<UserDto>();
      var connector = new Connector(userDto);

      //WHEN
      connector.ConnectWith(connecteeId); //bug what about connection in progress?

      //THEN
      userDto.Connections.Should().Contain(connecteeId);
    }
  }
}