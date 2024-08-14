using System.Globalization;
using FluentAssertions;
using NSubstitute;

namespace FlowSimulation;

public class SimulationDriver
{
  public const string Andy = "Andy";
  public const string Zenek = "Zenek";
  public const string Developer = "Developer";
  public const string CodeX = "Code X";
  public const string QA = "QA";
  public const string TestX = "Test X";
  public const string DeliverX = "Deliver X";
  public const string Johnny = "Johnny";
  public const string CodeY = "Code Y";
  public const string CodeZ = "Code Z";
  public const string TestY = "Test Y";
  public const string TestZ = "Test Z";
  public const string Sue = "Sue";
  public const string X = "X";
  public const string Y = "Y";
  public const string Z = "Z";
  public Simulation simulation;
  private readonly IEventsDestination additionalDestination;

  public SimulationDriver()
  {
    simulation = new Simulation();
    additionalDestination = Substitute.For<IEventsDestination>();
  }

  public void RunSimulation()
  {
    simulation.Run();
  }

  public void ShouldReportNoItemsInTheBacklog()
  {
    simulation.TextLog.AssertConsistsOf(["No items on the backlog"]);
  }
}

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
    var simulation = new Simulation();
    simulation.AddWorkItem(SimulationDriver.X);

    simulation.Run();

    simulation.TextLog.AssertConsistsOf(["No developers on the team"]);
  }

  [Test]
  public void ShouldMakeSingleDeveloperDoOnePointItemInSingleDay()
  {
    var simulation = new Simulation();
    simulation.AddWorkItem(SimulationDriver.X);
    simulation.AddTeamMember(SimulationDriver.Andy);

    simulation.Run();

    simulation.TextLog.AssertConsistsOf([
      Assigned(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
      InProgress(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
      Completed(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
    ]);
  }

  [Test]
  public void ShouldMakeSingleDeveloperDoTwoPointItemInTwoDays()
  {
    var simulation = new Simulation();
    simulation.AddWorkItem(SimulationDriver.X, WithPoints(2));
    simulation.AddTeamMember(SimulationDriver.Andy);

    simulation.Run();

    simulation.TextLog.AssertConsistsOf([
      Assigned(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
      InProgress(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
      InProgress(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
      Completed(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
    ]);
  }

  [Test]
  public void ShouldMakeOneDeveloperCompleteTwoWorkItems()
  {
    var simulation = new Simulation();
    simulation.AddWorkItem(SimulationDriver.X);
    simulation.AddWorkItem(SimulationDriver.Y);
    simulation.AddTeamMember(SimulationDriver.Andy);

    simulation.Run();

    simulation.TextLog.AssertConsistsOf([
      Assigned(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
      InProgress(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
      Completed(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
      Assigned(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Y),
      InProgress(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Y),
      Completed(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Y),
    ]);
  }


  [Test]
  public void ShouldMakeOneDeveloperSlackWhenThereAreTwoButOneWorkItem()
  {
    var simulation = new Simulation();
    simulation.AddWorkItem(SimulationDriver.X);
    simulation.AddTeamMember(SimulationDriver.Andy);
    simulation.AddTeamMember(SimulationDriver.Johnny);

    simulation.Run();

    simulation.TextLog.AssertConsistsOf([
      Assigned(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
      InProgress(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
      Slack(1, SimulationDriver.Johnny, SimulationDriver.Developer),
      Completed(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
    ]);
  }

  [Test]
  public void ShouldThrowExceptionWhenDeveloperIsAddedTwice()
  {
    var simulation = new Simulation();
    simulation.AddTeamMember(SimulationDriver.Andy);
    FluentActions.Invoking(() => simulation.AddTeamMember(SimulationDriver.Andy))
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenWorkItemIsAddedTwice()
  {
    var simulation = new Simulation();
    simulation.AddWorkItem(SimulationDriver.X);
    FluentActions.Invoking(() => simulation.AddWorkItem(SimulationDriver.X))
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenWorkItemDependsOnAnotherWithLowerPriority()
  {
    var simulation = new Simulation();
    simulation.AddTeamMember(SimulationDriver.Johnny);
    simulation.AddWorkItem(SimulationDriver.X);
    simulation.AddWorkItem(SimulationDriver.Y, new WorkItemProperties(priority: 4, dependencies: [SimulationDriver.X]));
    FluentActions.Invoking(simulation.Run)
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenWorkItemsHaveCircularDependency()
  {
    var simulation = new Simulation();
    simulation.AddTeamMember(SimulationDriver.Johnny);
    simulation.AddWorkItem(SimulationDriver.X, DependingOn(SimulationDriver.Y));
    simulation.AddWorkItem(SimulationDriver.Y, DependingOn(SimulationDriver.Z));
    simulation.AddWorkItem(SimulationDriver.Z, DependingOn(SimulationDriver.X));
    FluentActions.Invoking(simulation.Run)
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenWorkItemsDependOnNonExistentItems()
  {
    var simulation = new Simulation();
    simulation.AddTeamMember(SimulationDriver.Johnny);
    simulation.AddWorkItem(SimulationDriver.X, DependingOn(SimulationDriver.Y));
    simulation.AddWorkItem(SimulationDriver.Y, DependingOn(SimulationDriver.Z));
    FluentActions.Invoking(simulation.Run)
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenTeamDoesNotHaveARoleRequiredForATask()
  {
    var simulation = new Simulation();
    simulation.AddTeamMember(SimulationDriver.Johnny);
    simulation.AddWorkItem(SimulationDriver.X, new WorkItemProperties(requiredRole: SimulationDriver.QA));
    FluentActions.Invoking(simulation.Run)
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldMakeSingleDeveloperWorkOnTheTaskWithHigherPriorityFirst()
  {
    var simulation = new Simulation();

    simulation.AddWorkItem(SimulationDriver.X, WithPriority(3));
    simulation.AddWorkItem(SimulationDriver.Y, WithPriority(2));
    simulation.AddWorkItem(SimulationDriver.Z, WithPriority(1));

    simulation.AddTeamMember(SimulationDriver.Andy);

    simulation.Run();

    simulation.TextLog.AssertConsistsOf([
        Assigned(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Z),
        InProgress(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Z),
        Completed(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Z),
        Assigned(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Y),
        InProgress(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Y),
        Completed(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Y),
        Assigned(3, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
        InProgress(3, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
        Completed(3, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
      ]);
  }

  private static WorkItemProperties WithPriority(int priority)
  {
    return new WorkItemProperties(priority: priority);
  }

  [Test]
  public void ShouldMakeSingleDeveloperWorkOnDependentTasksAfterDependenciesAreFinished()
  {
    var simulation = new Simulation();

    simulation.AddWorkItem(SimulationDriver.X, DependingOn(SimulationDriver.Y));
    simulation.AddWorkItem(SimulationDriver.Y, DependingOn(SimulationDriver.Z));
    simulation.AddWorkItem(SimulationDriver.Z);

    simulation.AddTeamMember(SimulationDriver.Andy);

    simulation.Run();

    simulation.TextLog.AssertConsistsOf([
        Assigned(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Z),
        InProgress(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Z),
        Completed(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Z),
        Assigned(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Y),
        InProgress(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Y),
        Completed(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Y),
        Assigned(3, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
        InProgress(3, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
        Completed(3, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),

      ]);
  }

  [Test]
  public void ShouldAllowSimulatingHandoffs()
  {
    var simulation = new Simulation();

    simulation.AddWorkItem(SimulationDriver.CodeX, RequiresRole(SimulationDriver.Developer));
    simulation.AddWorkItem(SimulationDriver.TestX,
      new WorkItemProperties(requiredRole: SimulationDriver.QA, dependencies: [SimulationDriver.CodeX]));
    simulation.AddTeamMember(SimulationDriver.Andy, new TeamMemberProperties { Role = SimulationDriver.Developer });
    simulation.AddTeamMember(SimulationDriver.Sue, new TeamMemberProperties { Role = SimulationDriver.QA });

    simulation.Run();

    simulation.TextLog.AssertConsistsOf([
        Assigned(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeX),
        InProgress(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeX),
        Slack(1, SimulationDriver.Sue, SimulationDriver.QA),
        Completed(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeX),
        Assigned(2, SimulationDriver.Sue, SimulationDriver.QA, SimulationDriver.TestX),
        Slack(2, SimulationDriver.Andy, SimulationDriver.Developer),
        InProgress(2, SimulationDriver.Sue, SimulationDriver.QA, SimulationDriver.TestX),
        Completed(2, SimulationDriver.Sue, SimulationDriver.QA, SimulationDriver.TestX)
      ]);

  }

  [Test]
  public void ShouldMakeMultipleDevelopersWorkOnDependentTasksAfterDependenciesAreFinished()
  {
    var simulation = new Simulation();

    simulation.AddWorkItem(SimulationDriver.X, DependingOn(SimulationDriver.Z));
    simulation.AddWorkItem(SimulationDriver.Y, DependingOn(SimulationDriver.Z));
    simulation.AddWorkItem(SimulationDriver.Z);

    simulation.AddTeamMember(SimulationDriver.Andy);
    simulation.AddTeamMember(SimulationDriver.Johnny);

    simulation.Run();

    simulation.TextLog.AssertConsistsOf([
        Assigned(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Z),
        InProgress(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Z),
        Slack(1, SimulationDriver.Johnny, SimulationDriver.Developer),
        Completed(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.Z),
        Assigned(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
        Assigned(2, SimulationDriver.Johnny, SimulationDriver.Developer, SimulationDriver.Y),
        InProgress(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
        InProgress(2, SimulationDriver.Johnny, SimulationDriver.Developer, SimulationDriver.Y),
        Completed(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.X),
        Completed(2, SimulationDriver.Johnny, SimulationDriver.Developer, SimulationDriver.Y)
      ]);
  }

  [Test]
  public void ShouldSupportCombiningRolesWithDependenciesWithPoints()
  {
    var simulation = new Simulation();

    simulation.AddWorkItem(SimulationDriver.CodeX,
      new WorkItemProperties(requiredRole: SimulationDriver.Developer, points: 3));
    simulation.AddWorkItem(SimulationDriver.TestX,
      new WorkItemProperties(requiredRole: SimulationDriver.QA, dependencies: [SimulationDriver.CodeX]));
    simulation.AddWorkItem(SimulationDriver.CodeY,
      new WorkItemProperties(requiredRole: SimulationDriver.Developer, points: 3));
    simulation.AddWorkItem(SimulationDriver.TestY,
      new WorkItemProperties(requiredRole: SimulationDriver.QA, dependencies: [SimulationDriver.CodeY]));
    simulation.AddWorkItem(SimulationDriver.CodeZ,
      new WorkItemProperties(requiredRole: SimulationDriver.Developer, points: 3));
    simulation.AddWorkItem(SimulationDriver.TestZ,
      new WorkItemProperties(requiredRole: SimulationDriver.QA, dependencies: [SimulationDriver.CodeZ]));

    simulation.AddTeamMember(SimulationDriver.Andy, new TeamMemberProperties { Role = SimulationDriver.Developer });
    simulation.AddTeamMember(SimulationDriver.Johnny, new TeamMemberProperties { Role = SimulationDriver.Developer });
    simulation.AddTeamMember(SimulationDriver.Zenek, new TeamMemberProperties { Role = SimulationDriver.QA });

    simulation.Run();

    simulation.TextLog.AssertConsistsOf([
      Assigned(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeX),
      Assigned(1, SimulationDriver.Johnny, SimulationDriver.Developer, SimulationDriver.CodeY),
      InProgress(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeX),
      InProgress(1, SimulationDriver.Johnny, SimulationDriver.Developer, SimulationDriver.CodeY),
      Slack(1, SimulationDriver.Zenek, SimulationDriver.QA),
      InProgress(2, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeX),
      InProgress(2, SimulationDriver.Johnny, SimulationDriver.Developer, SimulationDriver.CodeY),
      Slack(2, SimulationDriver.Zenek, SimulationDriver.QA),
      InProgress(3, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeX),
      InProgress(3, SimulationDriver.Johnny, SimulationDriver.Developer, SimulationDriver.CodeY),
      Slack(3, SimulationDriver.Zenek, SimulationDriver.QA),
      Completed(3, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeX),
      Completed(3, SimulationDriver.Johnny, SimulationDriver.Developer, SimulationDriver.CodeY),
      Assigned(4, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeZ),
      Assigned(4, SimulationDriver.Zenek, SimulationDriver.QA, SimulationDriver.TestX),
      InProgress(4, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeZ),
      Slack(4, SimulationDriver.Johnny, SimulationDriver.Developer),
      InProgress(4, SimulationDriver.Zenek, SimulationDriver.QA, SimulationDriver.TestX),
      Completed(4, SimulationDriver.Zenek, SimulationDriver.QA, SimulationDriver.TestX),
      Assigned(5, SimulationDriver.Zenek, SimulationDriver.QA, SimulationDriver.TestY),
      InProgress(5, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeZ),
      Slack(5, SimulationDriver.Johnny, SimulationDriver.Developer),
      InProgress(5, SimulationDriver.Zenek, SimulationDriver.QA, SimulationDriver.TestY),
      Completed(5, SimulationDriver.Zenek, SimulationDriver.QA, SimulationDriver.TestY),
      InProgress(6, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeZ),
      Slack(6, SimulationDriver.Johnny, SimulationDriver.Developer),
      Slack(6, SimulationDriver.Zenek, SimulationDriver.QA),
      Completed(6, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeZ),
      Assigned(7, SimulationDriver.Zenek, SimulationDriver.QA, SimulationDriver.TestZ),
      Slack(7, SimulationDriver.Andy, SimulationDriver.Developer),
      Slack(7, SimulationDriver.Johnny, SimulationDriver.Developer),
      InProgress(7, SimulationDriver.Zenek, SimulationDriver.QA, SimulationDriver.TestZ),
      Completed(7, SimulationDriver.Zenek, SimulationDriver.QA, SimulationDriver.TestZ),
    ]);

    //BUG: add top level tasks - not sure how. Maybe using the mapping from work item to something like "story id"
    //BUG: and stories as separate beings? We'll see...
  }

  [Test]
  public void ShouldSupportAggregatingItemGroups()
  { 
    //GIVEN
    var simulation = new Simulation();

    //bug 1) unique id
    //bug 2) child items must exist

    simulation.AddWorkItem(SimulationDriver.CodeX, 
      new WorkItemProperties(requiredRole: SimulationDriver.Developer));

    simulation.AddWorkItem(SimulationDriver.TestX,
      new WorkItemProperties(
        requiredRole: SimulationDriver.QA, 
        dependencies: [SimulationDriver.CodeX]));
    
    simulation.AddWorkItemGroup(SimulationDriver.DeliverX, [SimulationDriver.CodeX, SimulationDriver.TestX]); //BUG add assertions for group item:
    simulation.AddTeamMember(SimulationDriver.Andy, new TeamMemberProperties { Role = SimulationDriver.Developer });
    simulation.AddTeamMember(SimulationDriver.Zenek, new TeamMemberProperties { Role = SimulationDriver.QA });


    //WHEN
    simulation.Run();

    //THEN
    simulation.TextLog.AssertConsistsOf([
      Assigned(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeX),
      InProgress(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeX),
      Slack(1, SimulationDriver.Zenek, SimulationDriver.QA),
      Completed(1, SimulationDriver.Andy, SimulationDriver.Developer, SimulationDriver.CodeX),
      Assigned(2, SimulationDriver.Zenek, SimulationDriver.QA, SimulationDriver.TestX),
      Slack(2, SimulationDriver.Andy, SimulationDriver.Developer),
      InProgress(2, SimulationDriver.Zenek, SimulationDriver.QA, SimulationDriver.TestX),
      Completed(2, SimulationDriver.Zenek, SimulationDriver.QA, SimulationDriver.TestX),
      GroupItemDelivered(2, SimulationDriver.DeliverX, 2)
    ]);
  }

  private static string GroupItemDelivered(int day, string itemGroupId, int points)
  {
    return $"Day {day}: Item group {itemGroupId} for {points} points is completed";
  }

  private static string Completed(int day, string name, string role, string itemName)
  {
    return $"Day {day}: {role} {name} completed the task {itemName}";
  }

  private static string Slack(int day, string name, string role)
  {
    return $"Day {day}: {role} {name} has nothing to work on";
  }

  private static string InProgress(int day, string name, string role, string itemName)
  {
    return $"Day {day}: {role} {name} is working on task {itemName}";
  }

  private static string Assigned(int day, string name, string role, string itemId)
  {
    return $"Day {day}: {role} {name} was assigned to task {itemId}";
  }

  private static WorkItemProperties DependingOn(params ItemId[] taskNames)
  {
    return new WorkItemProperties(dependencies: [.. taskNames]);
  }

  private static WorkItemProperties WithPoints(int points)
  {
    return new WorkItemProperties { Points = points };
  }

  private static WorkItemProperties RequiresRole(string developer)
  {
    return new WorkItemProperties(requiredRole: developer);
  }

  //bug add handovers (e.g. developer is QA or programmer)
}