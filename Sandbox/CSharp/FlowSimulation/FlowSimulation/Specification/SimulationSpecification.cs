using FlowSimulation.ProductionCode;
using FlowSimulation.Specification.Automation;
using FluentAssertions;

namespace FlowSimulation.Specification;

[TestFixture(TestOf = typeof(Simulation))]
public class SimulationSpecification
{
  [Test]
  public void ShouldSayNothingHappenedWhenNoWorkItemsConfigured()
  {
    var driver = new SimulationDriver();

    driver.RunSimulation();

    driver.ShouldReportNoItemsInTheBacklog();

  }

  [Test]
  public void ShouldSayNoDevelopersWhenOnlyWorkItemsAdded()
  {
    var simulationDriver = new SimulationDriver();
    simulationDriver.AddWorkItem(Tasks.X);

    simulationDriver.RunSimulation();

    simulationDriver.AssertTextLogConsistsOf([ExpectedEvents.NoMembersOnTheTeam()]);
  }

  [Test]
  public void ShouldMakeSingleDeveloperDoOnePointItemInSingleDay()
  {
    var simulationDriver = new SimulationDriver();
    simulationDriver.AddWorkItem(Tasks.X);
    simulationDriver.AddTeamMember(Developers.Andy);

    simulationDriver.RunSimulation();

    //BUG REFACTOR THIS!!!
    simulationDriver.AssertTextLogConsistsOf(
    [
      ExpectedEvents.Assigned(1, Developers.Andy, Roles.Developer, Tasks.X), 
      ExpectedEvents.InProgress(1, Developers.Andy, Roles.Developer, Tasks.X),
      ExpectedEvents.Completed(1, Developers.Andy, Roles.Developer, Tasks.X)
    ]);

    simulationDriver.AssertEvents(
    [
      ExpectedEvents.Assigned(1, Developers.Andy, Roles.Developer, Tasks.X), 
      ExpectedEvents.InProgress(1, Developers.Andy, Roles.Developer, Tasks.X),
      ExpectedEvents.Completed(1, Developers.Andy, Roles.Developer, Tasks.X),
      ExpectedEvents.NextDay()
    ]);
  }

  [Test]
  public void ShouldMakeSingleDeveloperDoTwoPointItemInTwoDays()
  {
    var simulationDriver = new SimulationDriver();
    simulationDriver.AddWorkItem(Tasks.X, WorkItemPropertiesMother.WithPoints(2));
    simulationDriver.AddTeamMember(Developers.Andy);

    simulationDriver.RunSimulation();

    simulationDriver.AssertTextLogConsistsOf([
      ExpectedEvents.Assigned(1, Developers.Andy, Roles.Developer, Tasks.X),
      ExpectedEvents.InProgress(1, Developers.Andy, Roles.Developer, Tasks.X),
      ExpectedEvents.InProgress(2, Developers.Andy, Roles.Developer, Tasks.X),
      ExpectedEvents.Completed(2, Developers.Andy, Roles.Developer, Tasks.X),
    ]);
  }


  [Test]
  public void ShouldMakeOneDeveloperCompleteTwoWorkItems()
  {
    var simulation = new SimulationDriver();
    simulation.AddWorkItem(Tasks.X);
    simulation.AddWorkItem(Tasks.Y);
    simulation.AddTeamMember(Developers.Andy);

    simulation.RunSimulation();

    simulation.AssertTextLogConsistsOf([
      ExpectedEvents.Assigned(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ExpectedEvents.InProgress(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ExpectedEvents.Completed(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ExpectedEvents.Assigned(2,
        Developers.Andy,
        Roles.Developer,
        Tasks.Y),
      ExpectedEvents.InProgress(2,
        Developers.Andy,
        Roles.Developer,
        Tasks.Y),
      ExpectedEvents.Completed(2,
        Developers.Andy,
        Roles.Developer,
        Tasks.Y),
    ]);
  }


  [Test]
  public void ShouldMakeOneDeveloperSlackWhenThereAreTwoButOneWorkItem()
  {
    var simulation = new SimulationDriver();
    simulation.AddWorkItem(Tasks.X);
    simulation.AddTeamMember(Developers.Andy);
    simulation.AddTeamMember(Developers.Johnny);

    simulation.RunSimulation();

    simulation.AssertTextLogConsistsOf([
      ExpectedEvents.Assigned(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ExpectedEvents.InProgress(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ExpectedEvents.Slack(1,
        Developers.Johnny,
        Roles.Developer),
      ExpectedEvents.Completed(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
    ]);
  }

  [Test]
  public void ShouldThrowExceptionWhenDeveloperIsAddedTwice()
  {
    var simulation = new SimulationDriver();
    simulation.AddTeamMember(Developers.Andy);
    FluentActions.Invoking(() => simulation.AddTeamMember(Developers.Andy))
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenWorkItemIsAddedTwice()
  {
    var simulation = new SimulationDriver();
    simulation.AddWorkItem(Tasks.X);
    FluentActions.Invoking(() => simulation.AddWorkItem(Tasks.X))
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenWorkItemDependsOnAnotherWithLowerPriority()
  {
    var simulation = new SimulationDriver();
    simulation.AddTeamMember(Developers.Johnny);
    simulation.AddWorkItem(Tasks.X);
    simulation.AddWorkItem(Tasks.Y, new WorkItemProperties(priority: 4, dependencies: [Tasks.X]));
    FluentActions.Invoking(simulation.RunSimulation)
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenWorkItemsHaveCircularDependency()
  {
    var simulation = new SimulationDriver();
    simulation.AddTeamMember(Developers.Johnny);
    simulation.AddWorkItem(Tasks.X, WorkItemPropertiesMother.DependingOn(Tasks.Y));
    simulation.AddWorkItem(Tasks.Y, WorkItemPropertiesMother.DependingOn(Tasks.Z));
    simulation.AddWorkItem(Tasks.Z, WorkItemPropertiesMother.DependingOn(Tasks.X));
    FluentActions.Invoking(simulation.RunSimulation)
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenWorkItemsDependOnNonExistentItems()
  {
    var simulation = new SimulationDriver();
    simulation.AddTeamMember(Developers.Johnny);
    simulation.AddWorkItem(Tasks.X, WorkItemPropertiesMother.DependingOn(Tasks.Y));
    simulation.AddWorkItem(Tasks.Y, WorkItemPropertiesMother.DependingOn(Tasks.Z));
    FluentActions.Invoking(simulation.RunSimulation)
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenTeamDoesNotHaveARoleRequiredForATask()
  {
    var simulation = new SimulationDriver();
    simulation.AddTeamMember(Developers.Johnny);
    simulation.AddWorkItem(Tasks.X, new WorkItemProperties(requiredRole: Roles.Qa));
    FluentActions.Invoking(simulation.RunSimulation)
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldMakeSingleDeveloperWorkOnTheTaskWithHigherPriorityFirst()
  {
    var simulation = new SimulationDriver();

    simulation.AddWorkItem(Tasks.X, WorkItemPropertiesMother.WithPriority(3));
    simulation.AddWorkItem(Tasks.Y, WorkItemPropertiesMother.WithPriority(2));
    simulation.AddWorkItem(Tasks.Z, WorkItemPropertiesMother.WithPriority(1));

    simulation.AddTeamMember(Developers.Andy);

    simulation.RunSimulation();

    simulation.AssertTextLogConsistsOf([
      ExpectedEvents.Assigned(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.Z),
      ExpectedEvents.InProgress(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.Z),
      ExpectedEvents.Completed(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.Z),
      ExpectedEvents.Assigned(2,
        Developers.Andy,
        Roles.Developer,
        Tasks.Y),
      ExpectedEvents.InProgress(2,
        Developers.Andy,
        Roles.Developer,
        Tasks.Y),
      ExpectedEvents.Completed(2,
        Developers.Andy,
        Roles.Developer,
        Tasks.Y),
      ExpectedEvents.Assigned(3,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ExpectedEvents.InProgress(3,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ExpectedEvents.Completed(3,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ]);
  }

  [Test]
  public void ShouldMakeSingleDeveloperWorkOnDependentTasksAfterDependenciesAreFinished()
  {
    var simulation = new SimulationDriver();

    simulation.AddWorkItem(Tasks.X, WorkItemPropertiesMother.DependingOn(Tasks.Y));
    simulation.AddWorkItem(Tasks.Y, WorkItemPropertiesMother.DependingOn(Tasks.Z));
    simulation.AddWorkItem(Tasks.Z);

    simulation.AddTeamMember(Developers.Andy);

    simulation.RunSimulation();

    simulation.AssertTextLogConsistsOf([
      ExpectedEvents.Assigned(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.Z),
      ExpectedEvents.InProgress(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.Z),
      ExpectedEvents.Completed(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.Z),
      ExpectedEvents.Assigned(2,
        Developers.Andy,
        Roles.Developer,
        Tasks.Y),
      ExpectedEvents.InProgress(2,
        Developers.Andy,
        Roles.Developer,
        Tasks.Y),
      ExpectedEvents.Completed(2,
        Developers.Andy,
        Roles.Developer,
        Tasks.Y),
      ExpectedEvents.Assigned(3,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ExpectedEvents.InProgress(3,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ExpectedEvents.Completed(3,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ]);
  }

  [Test]
  public void ShouldAllowSimulatingHandoffs()
  {
    var simulation = new SimulationDriver();

    simulation.AddWorkItem(Tasks.CodeX, WorkItemPropertiesMother.RequiresRole(Roles.Developer));
    simulation.AddWorkItem(Tasks.TestX,
      new WorkItemProperties(requiredRole: Roles.Qa, dependencies: [Tasks.CodeX]));
    simulation.AddTeamMember(Developers.Andy, new TeamMemberProperties { Role = Roles.Developer });
    simulation.AddTeamMember(Developers.Sue, new TeamMemberProperties { Role = Roles.Qa });

    simulation.RunSimulation();

    simulation.AssertTextLogConsistsOf([
      ExpectedEvents.Assigned(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeX),
      ExpectedEvents.InProgress(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeX),
      ExpectedEvents.Slack(1,
        Developers.Sue,
        Roles.Qa),
      ExpectedEvents.Completed(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeX),
      ExpectedEvents.Assigned(2,
        Developers.Sue,
        Roles.Qa,
        Tasks.TestX),
      ExpectedEvents.Slack(2,
        Developers.Andy,
        Roles.Developer),
      ExpectedEvents.InProgress(2,
        Developers.Sue,
        Roles.Qa,
        Tasks.TestX),
      ExpectedEvents.Completed(2,
        Developers.Sue,
        Roles.Qa,
        Tasks.TestX)
      ]);
  }

  [Test]
  public void ShouldMakeMultipleDevelopersWorkOnDependentTasksAfterDependenciesAreFinished()
  {
    var simulation = new SimulationDriver();

    simulation.AddWorkItem(Tasks.X, WorkItemPropertiesMother.DependingOn(Tasks.Z));
    simulation.AddWorkItem(Tasks.Y, WorkItemPropertiesMother.DependingOn(Tasks.Z));
    simulation.AddWorkItem(Tasks.Z);

    simulation.AddTeamMember(Developers.Andy);
    simulation.AddTeamMember(Developers.Johnny);

    simulation.RunSimulation();

    simulation.AssertTextLogConsistsOf([
      ExpectedEvents.Assigned(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.Z),
      ExpectedEvents.InProgress(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.Z),
      ExpectedEvents.Slack(1,
        Developers.Johnny,
        Roles.Developer),
      ExpectedEvents.Completed(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.Z),
      ExpectedEvents.Assigned(2,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ExpectedEvents.Assigned(2,
        Developers.Johnny,
        Roles.Developer,
        Tasks.Y),
      ExpectedEvents.InProgress(2,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ExpectedEvents.InProgress(2,
        Developers.Johnny,
        Roles.Developer,
        Tasks.Y),
      ExpectedEvents.Completed(2,
        Developers.Andy,
        Roles.Developer,
        Tasks.X),
      ExpectedEvents.Completed(2,
        Developers.Johnny,
        Roles.Developer,
        Tasks.Y)
      ]);
  }

  [Test]
  public void ShouldSupportCombiningRolesWithDependenciesWithPoints()
  {
    var simulation = new SimulationDriver();

    simulation.AddWorkItem(Tasks.CodeX,
      new WorkItemProperties(requiredRole: Roles.Developer, points: 3));
    simulation.AddWorkItem(Tasks.TestX,
      new WorkItemProperties(requiredRole: Roles.Qa, dependencies: [Tasks.CodeX]));
    simulation.AddWorkItem(Tasks.CodeY,
      new WorkItemProperties(requiredRole: Roles.Developer, points: 3));
    simulation.AddWorkItem(Tasks.TestY,
      new WorkItemProperties(requiredRole: Roles.Qa, dependencies: [Tasks.CodeY]));
    simulation.AddWorkItem(Tasks.CodeZ,
      new WorkItemProperties(requiredRole: Roles.Developer, points: 3));
    simulation.AddWorkItem(Tasks.TestZ,
      new WorkItemProperties(requiredRole: Roles.Qa, dependencies: [Tasks.CodeZ]));

    simulation.AddTeamMember(Developers.Andy, new TeamMemberProperties { Role = Roles.Developer });
    simulation.AddTeamMember(Developers.Johnny, new TeamMemberProperties { Role = Roles.Developer });
    simulation.AddTeamMember(Developers.Zenek, new TeamMemberProperties { Role = Roles.Qa });

    simulation.RunSimulation();

    simulation.AssertTextLogConsistsOf([
      ExpectedEvents.Assigned(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeX),
      ExpectedEvents.Assigned(1,
        Developers.Johnny,
        Roles.Developer,
        Tasks.CodeY),
      ExpectedEvents.InProgress(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeX),
      ExpectedEvents.InProgress(1,
        Developers.Johnny,
        Roles.Developer,
        Tasks.CodeY),
      ExpectedEvents.Slack(1,
        Developers.Zenek,
        Roles.Qa),
      ExpectedEvents.InProgress(2,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeX),
      ExpectedEvents.InProgress(2,
        Developers.Johnny,
        Roles.Developer,
        Tasks.CodeY),
      ExpectedEvents.Slack(2,
        Developers.Zenek,
        Roles.Qa),
      ExpectedEvents.InProgress(3,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeX),
      ExpectedEvents.InProgress(3,
        Developers.Johnny,
        Roles.Developer,
        Tasks.CodeY),
      ExpectedEvents.Slack(3,
        Developers.Zenek,
        Roles.Qa),
      ExpectedEvents.Completed(3,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeX),
      ExpectedEvents.Completed(3,
        Developers.Johnny,
        Roles.Developer,
        Tasks.CodeY),
      ExpectedEvents.Assigned(4,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeZ),
      ExpectedEvents.Assigned(4,
        Developers.Zenek,
        Roles.Qa,
        Tasks.TestX),
      ExpectedEvents.InProgress(4,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeZ),
      ExpectedEvents.Slack(4,
        Developers.Johnny,
        Roles.Developer),
      ExpectedEvents.InProgress(4,
        Developers.Zenek,
        Roles.Qa,
        Tasks.TestX),
      ExpectedEvents.Completed(4,
        Developers.Zenek,
        Roles.Qa,
        Tasks.TestX),
      ExpectedEvents.Assigned(5,
        Developers.Zenek,
        Roles.Qa,
        Tasks.TestY),
      ExpectedEvents.InProgress(5,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeZ),
      ExpectedEvents.Slack(5,
        Developers.Johnny,
        Roles.Developer),
      ExpectedEvents.InProgress(5,
        Developers.Zenek,
        Roles.Qa,
        Tasks.TestY),
      ExpectedEvents.Completed(5,
        Developers.Zenek,
        Roles.Qa,
        Tasks.TestY),
      ExpectedEvents.InProgress(6,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeZ),
      ExpectedEvents.Slack(6,
        Developers.Johnny,
        Roles.Developer),
      ExpectedEvents.Slack(6,
        Developers.Zenek,
        Roles.Qa),
      ExpectedEvents.Completed(6,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeZ),
      ExpectedEvents.Assigned(7,
        Developers.Zenek,
        Roles.Qa,
        Tasks.TestZ),
      ExpectedEvents.Slack(7,
        Developers.Andy,
        Roles.Developer),
      ExpectedEvents.Slack(7,
        Developers.Johnny,
        Roles.Developer),
      ExpectedEvents.InProgress(7,
        Developers.Zenek,
        Roles.Qa,
        Tasks.TestZ),
      ExpectedEvents.Completed(7,
        Developers.Zenek,
        Roles.Qa,
        Tasks.TestZ),
    ]);

    //BUG: add top level tasks - not sure how. Maybe using the mapping from work item to something like "story id"
    //BUG: and stories as separate beings? We'll see...
  }

  [Test]
  public void ShouldSupportAggregatingItemGroups()
  {
    //GIVEN
    var simulation = new SimulationDriver();

    //bug 1) unique id
    //bug 2) child items must exist

    simulation.AddWorkItem(Tasks.CodeX,
      new WorkItemProperties(requiredRole: Roles.Developer));

    simulation.AddWorkItem(Tasks.TestX,
      new WorkItemProperties(
        requiredRole: Roles.Qa,
        dependencies: [Tasks.CodeX]));

    simulation.AddWorkItemGroup(Tasks.DeliverX, [Tasks.CodeX, Tasks.TestX]); //BUG add assertions for group item:
    simulation.AddTeamMember(Developers.Andy, new TeamMemberProperties { Role = Roles.Developer });
    simulation.AddTeamMember(Developers.Zenek, new TeamMemberProperties { Role = Roles.Qa });


    //WHEN
    simulation.RunSimulation();

    //THEN
    simulation.AssertTextLogConsistsOf([
      ExpectedEvents.Assigned(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeX),
      ExpectedEvents.InProgress(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeX),
      ExpectedEvents.Slack(1,
        Developers.Zenek,
        Roles.Qa),
      ExpectedEvents.Completed(1,
        Developers.Andy,
        Roles.Developer,
        Tasks.CodeX),
      ExpectedEvents.Assigned(2,
        Developers.Zenek,
        Roles.Qa,
        Tasks.TestX),
      ExpectedEvents.Slack(2,
        Developers.Andy,
        Roles.Developer),
      ExpectedEvents.InProgress(2,
        Developers.Zenek,
        Roles.Qa,
        Tasks.TestX),
      ExpectedEvents.Completed(2,
        Developers.Zenek,
        Roles.Qa,
        Tasks.TestX),
      ExpectedEvents.GroupItemDelivered(2, Tasks.DeliverX, 2)
    ]);
  }

  //bug add handovers (e.g. developer is QA or programmer)
}