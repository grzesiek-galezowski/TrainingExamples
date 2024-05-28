using FluentAssertions;

namespace FlowSimulation;

[TestFixture(TestOf = typeof(Simulation))]
public class SimulationSpecification
{
  private const string Andy = "Andy";
  private const string Zenek = "Zenek";
  private const string Developer = "Developer";
  private const string CodeX = "Code X";
  private const string QA = "QA";
  private const string TestX = "Test X";
  private const string DeliverX = "Deliver X";
  private const string Johnny = "Johnny";
  private const string CodeY = "Code Y";
  private const string CodeZ = "Code Z";
  private const string TestY = "Test Y";
  private const string TestZ = "Test Z";
  private const string Sue = "Sue";

  [Test]
  public void ShouldSayNothingHappenedWhenNoWorkItemsConfigured()
  {
    var simulation = new Simulation();

    simulation.Run();

    AssertLog(simulation, ["No items on the backlog"]);
  }

  [Test]
  public void ShouldSayNoDevelopersWhenOnlyWorkItemsAdded()
  {
    var simulation = new Simulation();
    simulation.AddWorkItem("X");

    simulation.Run();

    AssertLog(simulation, ["No developers on the team"]);
  }

  [Test]
  public void ShouldMakeSingleDeveloperDoOnePointItemInSingleDay()
  {
    var simulation = new Simulation();
    simulation.AddWorkItem("X");
    simulation.AddTeamMember(Andy);

    simulation.Run();

    AssertLog(simulation, [
      Assigned(1, Andy, Developer, "X"),
      InProgress(1, Andy, Developer, "X"),
      Completed(1, Andy, Developer, "X"),
    ]);
  }

  [Test]
  public void ShouldMakeSingleDeveloperDoTwoPointItemInTwoDays()
  {
    var simulation = new Simulation();
    simulation.AddWorkItem("X", WithPoints(2));
    simulation.AddTeamMember(Andy);

    simulation.Run();

    AssertLog(simulation, [
      Assigned(1, Andy, Developer, "X"),
      InProgress(1, Andy, Developer, "X"),
      InProgress(2, Andy, Developer, "X"),
      Completed(2, Andy, Developer, "X"),
    ]);
  }

  [Test]
  public void ShouldMakeOneDeveloperCompleteTwoWorkItems()
  {
    var simulation = new Simulation();
    simulation.AddWorkItem("X");
    simulation.AddWorkItem("Y");
    simulation.AddTeamMember(Andy);

    simulation.Run();

    AssertLog(simulation, [
      Assigned(1, Andy, Developer, "X"),
      InProgress(1, Andy, Developer, "X"),
      Completed(1, Andy, Developer, "X"),
      Assigned(2, Andy, Developer, "Y"),
      InProgress(2, Andy, Developer, "Y"),
      Completed(2, Andy, Developer, "Y"),
    ]);
  }


  [Test]
  public void ShouldMakeOneDeveloperSlackWhenThereAreTwoButOneWorkItem()
  {
    var simulation = new Simulation();
    simulation.AddWorkItem("X");
    simulation.AddTeamMember(Andy);
    simulation.AddTeamMember(Johnny);

    simulation.Run();

    AssertLog(simulation, [
      Assigned(1, Andy, Developer, "X"),
      InProgress(1, Andy, Developer, "X"),
      Slack(1, Johnny, Developer),
      Completed(1, Andy, Developer, "X"),
    ]);
  }

  [Test]
  public void ShouldThrowExceptionWhenDeveloperIsAddedTwice()
  {
    var simulation = new Simulation();
    simulation.AddTeamMember(Andy);
    FluentActions.Invoking(() => simulation.AddTeamMember(Andy))
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenWorkItemIsAddedTwice()
  {
    var simulation = new Simulation();
    simulation.AddWorkItem("X");
    FluentActions.Invoking(() => simulation.AddWorkItem("X"))
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenWorkItemDependsOnAnotherWithLowerPriority()
  {
    var simulation = new Simulation();
    simulation.AddTeamMember(Johnny);
    simulation.AddWorkItem("X");
    simulation.AddWorkItem("Y", new WorkItemProperties
    {
      Priority = 4, Dependencies = ["X"]
    });
    FluentActions.Invoking(simulation.Run)
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenWorkItemsHaveCircularDependency()
  {
    var simulation = new Simulation();
    simulation.AddTeamMember(Johnny);
    simulation.AddWorkItem("X", DependingOn("Y"));
    simulation.AddWorkItem("Y", DependingOn("Z"));
    simulation.AddWorkItem("Z", DependingOn("X"));
    FluentActions.Invoking(simulation.Run)
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenWorkItemsDependOnNonExistentItems()
  {
    var simulation = new Simulation();
    simulation.AddTeamMember(Johnny);
    simulation.AddWorkItem("X", DependingOn("Y"));
    simulation.AddWorkItem("Y", DependingOn("Z"));
    FluentActions.Invoking(simulation.Run)
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldThrowExceptionWhenTeamDoesNotHaveARoleRequiredForATask()
  {
    var simulation = new Simulation();
    simulation.AddTeamMember(Johnny);
    simulation.AddWorkItem("X", new WorkItemProperties
    {
      RequiredRole = QA
    });
    FluentActions.Invoking(simulation.Run)
      .Should().Throw<Exception>();
  }

  [Test]
  public void ShouldMakeSingleDeveloperWorkOnTheTaskWithHigherPriorityFirst()
  {
    var simulation = new Simulation();

    simulation.AddWorkItem("X", WithPriority(3));
    simulation.AddWorkItem("Y", WithPriority(2));
    simulation.AddWorkItem("Z", WithPriority(1));

    simulation.AddTeamMember(Andy);

    simulation.Run();

    AssertLog(simulation, [
        Assigned(1, Andy, Developer, "Z"),
        InProgress(1, Andy, Developer, "Z"),
        Completed(1, Andy, Developer, "Z"),
        Assigned(2, Andy, Developer, "Y"),
        InProgress(2, Andy, Developer, "Y"),
        Completed(2, Andy, Developer, "Y"),
        Assigned(3, Andy, Developer, "X"),
        InProgress(3, Andy, Developer, "X"),
        Completed(3, Andy, Developer, "X"),
      ]
    );
  }

  private static WorkItemProperties WithPriority(int priority)
  {
    return new WorkItemProperties { Priority = priority };
  }

  [Test]
  public void ShouldMakeSingleDeveloperWorkOnDependentTasksAfterDependenciesAreFinished()
  {
    var simulation = new Simulation();

    simulation.AddWorkItem("X", DependingOn("Y"));
    simulation.AddWorkItem("Y", DependingOn("Z"));
    simulation.AddWorkItem("Z");

    simulation.AddTeamMember(Andy);

    simulation.Run();

    AssertLog(simulation, [
        Assigned(1, Andy, Developer, "Z"),
        InProgress(1, Andy, Developer, "Z"),
        Completed(1, Andy, Developer, "Z"),
        Assigned(2, Andy, Developer, "Y"),
        InProgress(2, Andy, Developer, "Y"),
        Completed(2, Andy, Developer, "Y"),
        Assigned(3, Andy, Developer, "X"),
        InProgress(3, Andy, Developer, "X"),
        Completed(3, Andy, Developer, "X"),

      ]
    );
  }

  [Test]
  public void ShouldAllowSimulatingHandoffs()
  {
    var simulation = new Simulation();

    simulation.AddWorkItem(CodeX, RequiresRole(Developer));
    simulation.AddWorkItem(TestX, new WorkItemProperties
    {
      RequiredRole = QA,
      Dependencies = [CodeX]
    });
    simulation.AddTeamMember(Andy, new TeamMemberProperties { Role = Developer });
    simulation.AddTeamMember(Sue, new TeamMemberProperties { Role = QA });

    simulation.Run();

    AssertLog(simulation, [
        Assigned(1, Andy, Developer, CodeX),
        InProgress(1, Andy, Developer, CodeX),
        Slack(1, Sue, QA),
        Completed(1, Andy, Developer, CodeX),
        Assigned(2, Sue, QA, TestX),
        Slack(2, Andy, Developer),
        InProgress(2, Sue, QA, TestX),
        Completed(2, Sue, QA, TestX)
      ]
    );

  }

  [Test]
  public void ShouldMakeMultipleDevelopersWorkOnDependentTasksAfterDependenciesAreFinished()
  {
    var simulation = new Simulation();

    simulation.AddWorkItem("X", DependingOn("Z"));
    simulation.AddWorkItem("Y", DependingOn("Z"));
    simulation.AddWorkItem("Z");

    simulation.AddTeamMember(Andy);
    simulation.AddTeamMember(Johnny);

    simulation.Run();

    AssertLog(simulation, [
        Assigned(1, Andy, Developer, "Z"),
        InProgress(1, Andy, Developer, "Z"),
        Slack(1, Johnny, Developer),
        Completed(1, Andy, Developer, "Z"),
        Assigned(2, Andy, Developer, "X"),
        Assigned(2, Johnny, Developer, "Y"),
        InProgress(2, Andy, Developer, "X"),
        InProgress(2, Johnny, Developer, "Y"),
        Completed(2, Andy, Developer, "X"),
        Completed(2, Johnny, Developer, "Y")
      ]
    );
  }

  [Test]
  public void ShouldSupportCombiningRolesWithDependenciesWithPoints()
  {
    var simulation = new Simulation();

    simulation.AddWorkItem(CodeX, new WorkItemProperties { RequiredRole = Developer, Points = 3 });
    simulation.AddWorkItem(TestX, new WorkItemProperties { RequiredRole = QA, Dependencies = [CodeX] });
    simulation.AddWorkItem(CodeY, new WorkItemProperties { RequiredRole = Developer, Points = 3 });
    simulation.AddWorkItem(TestY, new WorkItemProperties { RequiredRole = QA, Dependencies = [CodeY] });
    simulation.AddWorkItem(CodeZ, new WorkItemProperties { RequiredRole = Developer, Points = 3 });
    simulation.AddWorkItem(TestZ, new WorkItemProperties { RequiredRole = QA, Dependencies = [CodeZ] });

    simulation.AddTeamMember(Andy, new TeamMemberProperties { Role = Developer });
    simulation.AddTeamMember(Johnny, new TeamMemberProperties { Role = Developer });
    simulation.AddTeamMember(Zenek, new TeamMemberProperties { Role = QA });

    simulation.Run();

    AssertLog(simulation, [
      Assigned(1, Andy, Developer, CodeX),
      Assigned(1, Johnny, Developer, CodeY),
      InProgress(1, Andy, Developer, CodeX),
      InProgress(1, Johnny, Developer, CodeY),
      Slack(1, Zenek, QA),
      InProgress(2, Andy, Developer, CodeX),
      InProgress(2, Johnny, Developer, CodeY),
      Slack(2, Zenek, QA),
      InProgress(3, Andy, Developer, CodeX),
      InProgress(3, Johnny, Developer, CodeY),
      Slack(3, Zenek, QA),
      Completed(3, Andy, Developer, CodeX),
      Completed(3, Johnny, Developer, CodeY),
      Assigned(4, Andy, Developer, CodeZ),
      Assigned(4, Zenek, QA, TestX),
      InProgress(4, Andy, Developer, CodeZ),
      Slack(4, Johnny, Developer),
      InProgress(4, Zenek, QA, TestX),
      Completed(4, Zenek, QA, TestX),
      Assigned(5, Zenek, QA, TestY),
      InProgress(5, Andy, Developer, CodeZ),
      Slack(5, Johnny, Developer),
      InProgress(5, Zenek, QA, TestY),
      Completed(5, Zenek, QA, TestY),
      InProgress(6, Andy, Developer, CodeZ),
      Slack(6, Johnny, Developer),
      Slack(6, Zenek, QA),
      Completed(6, Andy, Developer, CodeZ),
      Assigned(7, Zenek, QA, TestZ),
      Slack(7, Andy, Developer),
      Slack(7, Johnny, Developer),
      InProgress(7, Zenek, QA, TestZ),
      Completed(7, Zenek, QA, TestZ),
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

    simulation.AddWorkItem(CodeX, new WorkItemProperties
    {
      RequiredRole = Developer
    });

    simulation.AddWorkItem(TestX, new WorkItemProperties
    {
      RequiredRole = QA,
      Dependencies = [CodeX]
    });
    
    simulation.AddWorkItemGroup(DeliverX, [CodeX, TestX]); //BUG add assertions for group item:
    simulation.AddTeamMember(Andy, new TeamMemberProperties { Role = Developer });
    simulation.AddTeamMember(Zenek, new TeamMemberProperties { Role = QA });


    //WHEN
    simulation.Run();

    //THEN
    AssertLog(simulation, [
      Assigned(1, Andy, Developer, CodeX),
      InProgress(1, Andy, Developer, CodeX),
      Slack(1, Zenek, QA),
      Completed(1, Andy, Developer, CodeX),
      Assigned(2, Zenek, QA, TestX),
      Slack(2, Andy, Developer),
      InProgress(2, Zenek, QA, TestX),
      Completed(2, Zenek, QA, TestX),
      GroupItemDelivered(2, DeliverX, 2)
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

  private static void AssertLog(Simulation simulation, string[] entries)
  {
    simulation.Events.Entries.Should().Equal(entries);
  }

  private static WorkItemProperties DependingOn(params ItemId[] taskNames)
  {
    return new WorkItemProperties
    {
      Dependencies = [.. taskNames]
    };
  }

  private static WorkItemProperties WithPoints(int points)
  {
    return new WorkItemProperties { Points = points };
  }

  private static WorkItemProperties RequiresRole(string developer)
  {
    return new WorkItemProperties { RequiredRole = developer };
  }

  //bug add handovers (e.g. developer is QA or programmer)
}