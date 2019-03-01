using System;
using FluentAssertions;
using NSubstitute;
using PloehKata;
using TddXt.AnyRoot;
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
      var userDto = Any.Instance<UserDto>();
      var connector = new Connector(userDto);
      var connecteeDto = Any.Instance<UserDto>();

      //WHEN
      connector.AddConnection(connecteeDto);

      //THEN
      userDto.Connections.Should().Contain(connecteeDto);
    }

    [Fact]
    public void ShouldSaveUserDtoToDestination()
    {
        //GIVEN
        var userDto = Any.Instance<UserDto>();
        var connector = new Connector(userDto);
        var destination = Substitute.For<IConnectorDestination>();

        //WHEN
        connector.WriteTo(destination);

        //THEN
        destination.Received(1).Save(userDto);
    }
  }
}