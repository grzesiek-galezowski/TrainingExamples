using PloehKata.Ports;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using Xunit;

namespace PloehKataSpecification.ApplicationLogicSpecification
{
  public class ConnectingUsersSpecification
  {
    [Fact]
    public void ShouldSaveUserWithConnectionToOtherAndReturnOtherUserWithConnectionToUserWhenBothUsersExist()
    {
      //GIVEN
      var user1 = Root.Any.Instance<UserDto>();
      var user2 = Root.Any.Instance<UserDto>();
      var context = new ApplicationLogicDriver();
      context.Start();

      context.UsersDatabaseContains(user1);
      context.UsersDatabaseContains(user2);

      //WHEN
      context.MakeAConnectionBetween(user1.Id, user2.Id);

      //THEN
      context.DatabaseShouldBeUpdatedWithConnectionFrom(user2, user1);
      context.ResponseShouldContainConnectionFrom(user1, user2);
    }

    [Fact]
    public void ShouldRespondThatUserNotFoundWhenConnectorDoesNotExist()
    {
      //GIVEN
      var user2 = Root.Any.Instance<UserDto>();
      var context = new ApplicationLogicDriver();
      context.Start();

      context.UsersDatabaseContains(user2);

      //WHEN
      context.MakeAConnectionBetween(Root.Any.String(), user2.Id);

      //THEN
      context.DatabaseShouldNotBeUpdated();
      context.ResponseShouldSayUserNotFound();
    }

    [Fact]
    public void ShouldRespondThatOtherUserNotFoundWhenConnecteeDoesNotExist()
    {
      //GIVEN
      var user1 = Root.Any.Instance<UserDto>();
      var context = new ApplicationLogicDriver();
      context.Start();
      context.UsersDatabaseContains(user1);

      //WHEN
      context.MakeAConnectionBetween(user1.Id, Root.Any.String());

      //THEN
      context.ResponseShouldSayOtherUserNotFound();
    }

    [Fact]
    public void ShouldRespondThatUserIdIsInvalidWhenConnectorIdIsRejectedByDatabase()
    {
      //GIVEN
      var user2 = Root.Any.Instance<UserDto>();
      var context = new ApplicationLogicDriver();
      context.Start();
      var user1Id = Root.Any.String();

      context.UsersDatabaseContains(user2);
      context.UsersDatabaseRejects(user1Id);

      //WHEN
      context.MakeAConnectionBetween(user1Id, user2.Id);

      //THEN
      context.DatabaseShouldNotBeUpdated();
      context.ResponseShouldSayUserIdInvalid();
    }

    [Fact]
    public void ShouldRespondThatOtherUserIdIsInvalidWhenConnectorIdIsRejectedByDatabase()
    {
      //GIVEN
      var user1 = Root.Any.Instance<UserDto>();
      var context = new ApplicationLogicDriver();
      context.Start();
      var user2Id = Root.Any.String();

      context.UsersDatabaseContains(user1);
      context.UsersDatabaseRejects(user2Id);

      //WHEN
      context.MakeAConnectionBetween(user1.Id, user2Id);

      //THEN
      context.DatabaseShouldNotBeUpdated();
      context.ResponseShouldSayOtherUserIdInvalid();
    }


  }
}
