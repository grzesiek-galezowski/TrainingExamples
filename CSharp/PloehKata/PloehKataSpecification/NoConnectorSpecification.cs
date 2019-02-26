using System;
using FluentAssertions;
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
    public void ShouldReportThatUserIsNotFoundWhenAttemptingConnectionWithIt()
    {
      //GIVEN
      var noConnector = new NoConnector();
      var connectionInProgress = Substitute.For<IConnectionInProgress>();

      //WHEN
      noConnector.AttemptConnectionWith(Any.Exploding<IConnectee>(), connectionInProgress);

      //THEN
      XReceived.Only(() => connectionInProgress.UserNotFound());
    }

    [Fact]
    public void ShouldIgnoreRequestToWriteItToDestination()
    {
      //GIVEN
      var noConnector = new NoConnector();

      //WHEN - THEN
      new Action(() => noConnector.WriteTo(Any.Exploding<IConnectorDestination>()))
          .Should().NotThrow();
    }
  }
}