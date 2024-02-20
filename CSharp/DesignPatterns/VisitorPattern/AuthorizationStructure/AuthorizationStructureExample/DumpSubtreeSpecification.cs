using AuthorizationStructureExample.ProductionCode;
using NSubstitute.ClearExtensions;
using static AuthorizationStructureExample.ProductionCode.AuthorizationStructure;

namespace AuthorizationStructureExample;

public class DumpSubtreeSpecification
{
  [Test]
  public void ShouldIncludeOnlyElementsInASubtreeWhenDumpingIt()
  {
    //GIVEN
    var groupNotInSubtree = Any.String();
    var devNotInSubtree = Any.String();
    var userNotInSubtree = Any.String();
    var subtreeRoot = Any.String();
    var subtreeUser = Any.String();
    var target = Substitute.For<IChangeEventsTarget>();
    var s = new AuthorizationStructure(target);

    var subtreeDevice = Any.String();
    s.AddGroup(RootNodeId.Name, groupNotInSubtree);
    s.AddDevice(RootNodeId.Name, devNotInSubtree);
    s.AddUser(RootNodeId.Name, userNotInSubtree);
    s.AddGroup(groupNotInSubtree, subtreeRoot);
    s.AddUser(subtreeRoot, subtreeUser);
    s.AddDevice(subtreeRoot, subtreeDevice);
    target.ClearSubstitute();

    //WHEN
    s.DumpStartingFrom(NodeId.Group(subtreeRoot));

    //THEN
    XReceived.Exactly(() =>
    {
      target.Added(NodeId.Group(subtreeRoot), NodeId.Group(groupNotInSubtree).Just());
      target.Added(NodeId.User(subtreeUser), NodeId.Group(subtreeRoot).Just());
      target.Added(NodeId.Device(subtreeDevice), NodeId.Group(subtreeRoot).Just());
    });
  }

  //BUG: error handling, e.g. id does not exist
}