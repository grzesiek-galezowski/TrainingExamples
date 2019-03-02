using System;
using FluentAssertions;
using Functional.Maybe;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PloehKata.Adapters;
using PloehKata.Bootstrap;
using PloehKata.Ports;
using TddXt.AnyRoot;
using TddXt.XNSubstitute.Root;

namespace PloehKataSpecification.ApplicationLogicSpecification
{
  public class ApplicationLogicDriver
  {
    private readonly IPersistence _persistence = Substitute.For<IPersistence>();
    private ApplicationLogicRoot _applicationLogicRoot;
    private readonly IConnectionInProgress _connectionInProgress = Substitute.For<IActionResultBasedConnectionInProgress>();

    public void Start()
    {
      _applicationLogicRoot = new ApplicationLogicRoot(_persistence);
    }

    public void UsersDatabaseContains(UserDto user1)
    {
      _persistence.ReadById<UserDto>("Users", user1.Id).Returns(user1.ToMaybe());
    }

    public void MakeAConnectionBetween(string user1Id, string user2Id)
    {
      _applicationLogicRoot.GetUserUseCaseFactory()
        .CreateConnectionUseCase(user1Id, user2Id, _connectionInProgress).Execute();
    }

    public void DatabaseShouldBeUpdatedWithConnectionFrom(UserDto user2,
      UserDto user1)
    {
      XReceived.Only(() => _connectionInProgress.Success(Arg<UserDto>.That(dto =>
      {
        dto.Should().Be(user2);
        dto.Connections.Should().Contain(user1);
      })));
    }

    public void ResponseShouldContainConnectionFrom(UserDto user1, UserDto user2)
    {
      _persistence.Received(1).Save("Users", Arg<UserDto>.That(dto =>
      {
        dto.Should().Be(user1);
        dto.Connections.Should().Contain(user2);
      }));
    }

    public void DatabaseShouldNotBeUpdated()
    {
      _persistence.DidNotReceiveWithAnyArgs().Save(null, null);
    }

    public void ResponseShouldSayUserNotFound()
    {
      XReceived.Only(() => _connectionInProgress.Received(1).UserNotFound());
    }

    public void ResponseShouldSayOtherUserNotFound()
    {
      XReceived.Only(() => _connectionInProgress.Received(1).OtherUserNotFound());
    }
    public void ResponseShouldSayUserIdInvalid()
    {
      XReceived.Only(() => _connectionInProgress.Received(1).InvalidUserId());
    }

    public void ResponseShouldSayOtherUserIdInvalid()
    {
      XReceived.Only(() => _connectionInProgress.Received(1).InvalidOtherUserId());
    }

    public void UsersDatabaseRejects(string userId)
    {
      _persistence.ReadById<UserDto>("Users", userId).Throws(Root.Any.Instance<Exception>());
    }

  }
}