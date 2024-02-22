using AuthorizationStructureExampleWithVisitor.ProductionCode;
using FluentAssertions;
using static AuthorizationStructureExampleWithVisitor.ProductionCode.AuthorizationStructure;

namespace AuthorizationStructureExampleWithVisitor;

public class ExistenceCheckSpecification
{
  [Test]
  public void ShouldSayAddedElementsExist()
  {
    //GIVEN
    var group1 = Any.String();
    var group2 = Any.String();
    var group3 = Any.String();
    var user1 = Any.String();
    var device1 = Any.String();
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());
    
    s.AddGroup(RootNodeId.Name, group1);
    s.AddGroup(group1, group2);
    s.AddGroup(group1, group3);
    s.AddUser(group1, user1);
    s.AddDevice(group1, device1, Any.String());

    //WHEN
    var resultForGroup1 = s.Exists(NodeId.Group(group1));
    var resultForGroup2 = s.Exists(NodeId.Group(group2));
    var resultForGroup3 = s.Exists(NodeId.Group(group3));
    var resultForUser1 = s.Exists(NodeId.User(user1));
    var resultForDevice1 = s.Exists(NodeId.Device(device1));

    //THEN
    resultForGroup1.Should().BeTrue();
    resultForGroup2.Should().BeTrue();
    resultForGroup3.Should().BeTrue();
    resultForUser1.Should().BeTrue();
    resultForDevice1.Should().BeTrue();
  }

  [Test]
  public void ShouldSayRemovedGroupSubtreesDoNotExist()
  {
    //GIVEN
    var group1 = Any.String();
    var group2 = Any.String();
    var group3 = Any.String();
    var user1 = Any.String();
    var device1 = Any.String();
    var s = new AuthorizationStructure(Any.Instance<IChangeEventsTarget>());

    s.AddGroup(RootNodeId.Name, group1);
    s.AddGroup(group1, group2);
    s.AddGroup(group1, group3);
    s.AddUser(group1, user1);
    s.AddDevice(group1, device1, Any.String());
    s.Remove(NodeId.Group(group1));

    //WHEN
    var resultForGroup1 = s.Exists(NodeId.Group(group1));
    var resultForGroup2 = s.Exists(NodeId.Group(group2));
    var resultForGroup3 = s.Exists(NodeId.Group(group3));
    var resultForUser1 = s.Exists(NodeId.User(user1));
    var resultForDevice1 = s.Exists(NodeId.Device(device1));

    //THEN
    resultForGroup1.Should().BeFalse();
    resultForGroup2.Should().BeFalse();
    resultForGroup3.Should().BeFalse();
    resultForUser1.Should().BeFalse();
    resultForDevice1.Should().BeFalse();
  }
}