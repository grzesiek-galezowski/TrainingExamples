using FluentAssertions;
using PloehKata;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TddXt.XFluentAssert.Root;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace PloehKataSpecification
{
  public class UserInfrastructureSpecification
  {
    [Fact]
    public void ShouldCreateEmptyConnectionInProgress()
    {
      //GIVEN
      var userInfrastructure = new UserInfrastructure(Any.Instance<IUserRepository>());

      //WHEN
      var connectionInProgress = userInfrastructure.CreateConnectionInProgress();

      //THEN
      connectionInProgress.Should().BeOfType<ConnectionInProgress>();
    }

    [Fact]
    public void ShouldCreateConnectCommandWithIdsAndConnectionInProgress()
    {
      //GIVEN
      var repository = Any.Instance<IUserRepository>();
      var userInfrastructure = new UserInfrastructure(repository);
      var connectionInProgress = Any.Instance<IConnectionInProgress>();
      var user1Id = Any.String();
      var user2Id = Any.String();

      //WHEN
      var command = userInfrastructure.CreateConnectionCommand(connectionInProgress, user1Id, user2Id);

      //THEN
      command.Should().BeOfType<ConnectionCommand>()
        .And.DependOn(repository)
        .And.DependOn(user1Id)
        .And.DependOn(user2Id)
        .And.DependOn(connectionInProgress);
    }
  }
}