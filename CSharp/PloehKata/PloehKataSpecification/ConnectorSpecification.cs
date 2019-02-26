using System;
using NSubstitute;
using PloehKata;
using Xunit;

namespace PloehKataSpecification
{
  public class ConnectorSpecification //bug no connector
  {
    [Fact]
    public void ShouldDOWHAT() //bug
    {
      //GIVEN
      var connector = new Connector();
      var connectee = Substitute.For<IConnectee>();
      var connectionInProgress = Substitute.For<IConnectionInProgress>();

      //WHEN
      connector.AttemptConnectionWith(connectee, connectionInProgress);

      //THEN
      connectee.Received(1).AttemptConnectionFrom(connector, connectionInProgress);
    }
  }
}