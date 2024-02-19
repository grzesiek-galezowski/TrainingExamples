using AuthorizationStructure.ProductionCode;
using static AuthorizationStructure.ProductionCode.AuthorizationStructure;

namespace AuthorizationStructure;

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
    var target = Substitute.For<IDumpTarget>();
    var s = new ProductionCode.AuthorizationStructure();

    var subtreeDevice = Any.String();
    s.AddGroup(RootNodeId, groupNotInSubtree);
    s.AddDevice(RootNodeId, devNotInSubtree);
    s.AddUser(RootNodeId, userNotInSubtree);
    s.AddGroup(NodeId.Group(groupNotInSubtree), subtreeRoot);
    s.AddUser(NodeId.Group(subtreeRoot), subtreeUser);
    s.AddDevice(NodeId.Group(subtreeRoot), subtreeDevice);
    
    //WHEN
    s.DumpStartingFrom(NodeId.Group(subtreeRoot), target);

    //THEN
    XReceived.Exactly(() =>
    {
      target.Add(NodeId.Group(subtreeRoot), NodeId.Group(groupNotInSubtree).Just());
      target.Add(NodeId.User(subtreeUser), NodeId.Group(subtreeRoot).Just());
      target.Add(NodeId.Device(subtreeDevice), NodeId.Group(subtreeRoot).Just());
    });
  }

  //BUG: error handling, e.g. id does not exist
}