using DataAccess.Ports;
using Domain;
using NSubstitute;
using NUnit.Framework;

namespace ComponentSpecification
{
    public class TheOnlyPathSpecification
    {
      [Test]
    //bug this is not showing how to write good component specs
    //bug this is just an example showing that you can do this.
    public void ShouldSaveEmployeeInDatabaseWhenTriggeredFromView()
      {
        //GIVEN
        var persistentStorage 
           = Substitute.For<IPersistentStorage>();
        var app = new DomainLogic(persistentStorage);

        //THEN
        app.HandleAddEmployeeRequest();

        //THEN
        persistentStorage.Received(1).SaveEmployee();
      }
    }
}
