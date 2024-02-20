using System;
using AuthorizationStructureExample.ProductionCode;
using FluentAssertions;
using TddXt.AnyRoot;
using static AuthorizationStructureExample.ProductionCode.AuthorizationStructure;

namespace AuthorizationStructureExample;

public class InvalidOperationsSpecification
{
  [Test]
  public void ShouldThrowWhenTryingToAddTheSameGroupTwice()
  {
    //GIVEN
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    var group1 = Any.String();

    s.AddGroup(RootNodeId.Name, group1);

    //WHEN - THEN
    new Action(() => s.AddGroup(RootNodeId.Name, group1))
      .Should().Throw<InvalidOperationException>();
  }

  [Test]
  public void ShouldThrowWhenTryingToAddTheSameUserTwice()
  {
    //GIVEN
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    var user1 = Any.String();

    s.AddUser(RootNodeId.Name, user1);

    //WHEN - THEN
    new Action(() => s.AddUser(RootNodeId.Name, user1))
      .Should().Throw<InvalidOperationException>();
  }

  [Test]
  public void ShouldThrowWhenTryingToAddTheSameDeviceTwice()
  {
    //GIVEN
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    var device1 = Any.String();

    s.AddDevice(RootNodeId.Name, device1);

    //WHEN - THEN
    new Action(() => s.AddDevice(RootNodeId.Name, device1))
      .Should().Throw<InvalidOperationException>();
  }

  [Test]
  public void ShouldThrowWhenTryingToAddDeviceToNonExistentParent()
  {
    //GIVEN
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    var device1 = Any.String();

    //WHEN - THEN
    new Action(() => s.AddDevice(Any.OtherThan(RootNodeId.Name), device1))
      .Should().Throw<InvalidOperationException>();
  }

  [Test]
  public void ShouldThrowWhenTryingToAddUserToNonExistentParent()
  {
    //GIVEN
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    var user1 = Any.String();

    //WHEN - THEN
    new Action(() => s.AddUser(Any.OtherThan(RootNodeId.Name), user1))
      .Should().Throw<InvalidOperationException>();
  }

  [Test]
  public void ShouldThrowWhenTryingToAddGroupToNonExistentParent()
  {
    //GIVEN
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    var group1 = Any.String();

    //WHEN - THEN
    new Action(() => s.AddGroup(Any.OtherThan(RootNodeId.Name), group1))
      .Should().Throw<InvalidOperationException>();
  }

  [Test]
  public void ShouldThrowWhenTryingToDeleteRootNode()
  {
    //GIVEN
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    var group1 = Any.String();

    //WHEN - THEN
    new Action(() => s.Remove(RootNodeId))
      .Should().Throw<InvalidOperationException>();
  }

  //TODO: other situations where id or parent id does not exist
}