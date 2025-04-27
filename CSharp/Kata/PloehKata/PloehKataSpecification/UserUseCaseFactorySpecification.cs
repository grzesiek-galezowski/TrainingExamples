using FluentAssertions;
using PloehKata;
using PloehKata.Logic;
using PloehKata.Ports;
using TddXt.AnyRoot.Strings;
using TddXt.XFluentAssert.Root;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
  public class UserUseCaseFactorySpecification
  {
    [Fact]
    public void ShouldCreateConnectCommandWithIdsAndConnectionInProgress()
    {
      //GIVEN
      var repository = Any.Instance<IUserLookup>();
      var destination = Any.Instance<IConnectorDestination>();
      var factory = new UserUseCaseFactory(destination, repository);
      var connectionInProgress = Any.Instance<IConnectionInProgress>();
      var user1Id = Any.String();
      var user2Id = Any.String();

      //WHEN
      var command = factory.CreateConnectionUseCase(user1Id, user2Id, connectionInProgress);

      //THEN
      command.Should().BeOfType<ConnectionUseCase>()
        .And.DependOn(repository)
        .And.DependOn(destination)
        .And.DependOn(user1Id)
        .And.DependOn(user2Id)
        .And.DependOn(connectionInProgress);
    }
  }
}