using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PloehKata;
using PloehKata.Adapters;
using PloehKata.Ports;
using TddXt.AnyRoot.Strings;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
  public class ConnectionsControllerSpecification
  {
    [Fact]
    public void ShouldForwardLogicToConnectCommand()
    {
      //GIVEN
      var connectCommand = Substitute.For<IUserUseCase>();
      var resultFromConnection = Any.Instance<IActionResult>();
      var connectionInProgress = Substitute.For<IActionResultBasedConnectionInProgress>();
      var useCaseFactory = Substitute.For<IUserUseCaseFactory>();
      var connectionInProgressFactory = Substitute.For<IConnectionInProgressFactory>();
      var user1Id = Any.String();
      var user2Id = Any.String();
      var connectionsController = new ConnectionsController(
        useCaseFactory, connectionInProgressFactory);

      connectionInProgressFactory.CreateConnectionInProgress().Returns(connectionInProgress);
      useCaseFactory.CreateConnectionUseCase(user1Id, user2Id, connectionInProgress).Returns(connectCommand);
      connectionInProgress.ToActionResult().Returns(resultFromConnection);

      //WHEN
      var actionResult = connectionsController.Connect(user1Id, user2Id);

      //THEN
      connectCommand.Received(1).Execute();
      actionResult.Should().Be(resultFromConnection);
    }
  }
}