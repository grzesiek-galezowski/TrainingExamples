using System;
using NSubstitute;
using PloehKata;
using TddXt.AnyRoot.Exploding;
using TddXt.XNSubstitute.Root;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
  public class NoConnectorSpecification
  {
    [Fact]
    public void ShouldDOWHAT() //bug
    {
      //GIVEN
      var noConnector = new NoConnector();
      var connectee = Any.Exploding<IConnectee>();
      var connectionInProgress = Substitute.For<IConnectionInProgress>();

      //WHEN
      noConnector.AttemptConnectionWith(connectee, connectionInProgress);

      //THEN
      XReceived.Only(() => connectionInProgress.UserNotFound());
    }
  }
}