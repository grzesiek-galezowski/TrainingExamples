using AuthorizationStructureExampleWithVisitor.ProductionCode;
using FluentAssertions;
using static AuthorizationStructureExampleWithVisitor.ProductionCode.AuthorizationStructure;

namespace AuthorizationStructureExampleWithVisitor;

public class CheckingUserAccessSpecification
{
  [Test]
  public void ShouldReturnTrueWhenGroupContainsADevice()
  {
    //GIVEN
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    var device1 = Any.String();
    
    s.AddDevice(RootNodeId.Name, device1, Any.String());

    //WHEN
    var result = s.Contains(NodeId.Device(device1), RootNodeId.Name);

    //THEN
    result.Should().BeTrue();
  }

  [Test]
  public void ShouldReturnFalseWhenAskedWhetherGroupContainsUnknownDevice()
  {
    //GIVEN
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    var device1 = Any.String();
    
    //WHEN
    var result = s.Contains(NodeId.Device(device1), RootNodeId.Name);

    //THEN
    result.Should().BeFalse();
  }

  [Test]
  public void ShouldReturnTrueWhenAskedWhetherGroupContainsItself()
  {
    //GIVEN
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    
    //WHEN
    var result = s.Contains(RootNodeId, RootNodeId.Name);

    //THEN
    result.Should().BeTrue();
  }

  [Test]
  public void ShouldReturnTrueWhenAskedWhetherUserOwnsDeviceContainedByItsParent()
  {
    //GIVEN
    var user1 = Any.String();
    var device1 = Any.String();
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddUser(RootNodeId.Name, user1);
    s.AddDevice(RootNodeId.Name, device1, Any.String());

    //WHEN
    var result = s.IsOwnershipBetween(user1, device1);

    //THEN
    result.Should().BeTrue();
  }

  [Test]
  public void ShouldReturnFalseWhenAskedWhetherUserOwnsDeviceNotContainedByItsParent()
  {
    //GIVEN
    var user1 = Any.String();
    var device1 = Any.String();
    var subgroup1 = Any.String();
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddGroup(RootNodeId.Name, subgroup1);
    s.AddDevice(RootNodeId.Name, device1, Any.String());
    s.AddUser(subgroup1, user1);

    //WHEN
    var result = s.IsOwnershipBetween(user1, device1);

    //THEN
    result.Should().BeFalse();
  }

  [Test]
  public void ShouldReturnFalseWhenAskedWhetherGroupContainsDeletedUser()
  {
    //GIVEN
    var user1 = Any.String();
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddUser(RootNodeId.Name, user1);
    s.Remove(NodeId.User(user1));
    
    //WHEN
    var result = s.Contains(NodeId.User(user1), RootNodeId.Name);

    //THEN
    result.Should().BeFalse();
  }

  [Test]
  public void ShouldReturnFalseWhenAskedWhetherGroupContainsDeletedDevice()
  {
    //GIVEN
    var device1 = Any.String();
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddDevice(RootNodeId.Name, device1, Any.String());
    s.Remove(NodeId.Device(device1));
    
    //WHEN
    var result = s.Contains(NodeId.Device(device1), RootNodeId.Name);

    //THEN
    result.Should().BeFalse();
  }

  [Test]
  public void ShouldReturnFalseWhenAskedWhetherGroupContainsDeletedGroupWithSubgroups()
  {
    //GIVEN
    var group1 = Any.String();
    var group2 = Any.String();
    var group3 = Any.String();
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    s.AddGroup(RootNodeId.Name, group1);
    s.AddGroup(group1, group2);
    s.AddGroup(group1, group3);
    s.Remove(NodeId.Group(group1));
    
    //WHEN
    var resultForGroup1 = s.Contains(NodeId.Group(group1), RootNodeId.Name);
    var resultForGroup2 = s.Contains(NodeId.Group(group2), RootNodeId.Name);
    var resultForGroup3 = s.Contains(NodeId.Group(group3), RootNodeId.Name);

    //THEN
    resultForGroup1.Should().BeFalse();
    resultForGroup2.Should().BeFalse();
    resultForGroup3.Should().BeFalse();
  }
}