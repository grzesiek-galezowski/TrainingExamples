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
      "Day 1: Developer Andy was assigned to task X",
      "Day 1: Developer Andy is working on task X",
      "Day 1: Developer Andy completed the task X"
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
      "Day 1: Developer Andy was assigned to task X",
      "Day 1: Developer Andy is working on task X",
      "Day 2: Developer Andy is working on task X",
      "Day 2: Developer Andy completed the task X",
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
      "Day 1: Developer Andy was assigned to task X",
      "Day 1: Developer Andy is working on task X",
      "Day 1: Developer Andy completed the task X",
      "Day 2: Developer Andy was assigned to task Y",
      "Day 2: Developer Andy is working on task Y",
      "Day 2: Developer Andy completed the task Y"
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
      "Day 1: Developer Andy was assigned to task X",
      "Day 1: Developer Andy is working on task X",
      "Day 1: Developer Johnny has nothing to work on",
      "Day 1: Developer Andy completed the task X",
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
        "Day 1: Developer Andy was assigned to task Z",
        "Day 1: Developer Andy is working on task Z",
        "Day 1: Developer Andy completed the task Z",
        "Day 2: Developer Andy was assigned to task Y",
        "Day 2: Developer Andy is working on task Y",
        "Day 2: Developer Andy completed the task Y",
        "Day 3: Developer Andy was assigned to task X",
        "Day 3: Developer Andy is working on task X",
        "Day 3: Developer Andy completed the task X"
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
        "Day 1: Developer Andy was assigned to task Z",
        "Day 1: Developer Andy is working on task Z",
        "Day 1: Developer Andy completed the task Z",
        "Day 2: Developer Andy was assigned to task Y",
        "Day 2: Developer Andy is working on task Y",
        "Day 2: Developer Andy completed the task Y",
        "Day 3: Developer Andy was assigned to task X",
        "Day 3: Developer Andy is working on task X",
        "Day 3: Developer Andy completed the task X"
      ]
    );
  }

  [Test]
  public void ShouldAllowSimulatingHandoffs()
  {
    var simulation = new Simulation();

    simulation.AddWorkItem("Develop X", RequiresRole(Developer));
    simulation.AddWorkItem(TestX, new WorkItemProperties
    {
      RequiredRole = QA,
      Dependencies = ["Develop X"]
    });
    simulation.AddTeamMember(Andy, new TeamMemberProperties { Role = Developer });
    simulation.AddTeamMember("Sue", new TeamMemberProperties { Role = QA });

    simulation.Run();

    AssertLog(simulation, [
        "Day 1: Developer Andy was assigned to task Develop X",
        "Day 1: Developer Andy is working on task Develop X",
        "Day 1: QA Sue has nothing to work on",
        "Day 1: Developer Andy completed the task Develop X",
        "Day 2: QA Sue was assigned to task Test X",
        "Day 2: Developer Andy has nothing to work on",
        "Day 2: QA Sue is working on task Test X",
        "Day 2: QA Sue completed the task Test X"
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
        "Day 1: Developer Andy was assigned to task Z",
        "Day 1: Developer Andy is working on task Z",
        "Day 1: Developer Johnny has nothing to work on",
        "Day 1: Developer Andy completed the task Z",
        "Day 2: Developer Andy was assigned to task X",
        "Day 2: Developer Johnny was assigned to task Y",
        "Day 2: Developer Andy is working on task X",
        "Day 2: Developer Johnny is working on task Y",
        "Day 2: Developer Andy completed the task X",
        "Day 2: Developer Johnny completed the task Y"
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
    simulation.AddWorkItem("Test Y", new WorkItemProperties { RequiredRole = QA, Dependencies = [CodeY] });
    simulation.AddWorkItem("Code Z", new WorkItemProperties { RequiredRole = Developer, Points = 3 });
    simulation.AddWorkItem("Test Z", new WorkItemProperties { RequiredRole = QA, Dependencies = ["Code Z"] });

    simulation.AddTeamMember(Andy, new TeamMemberProperties { Role = Developer });
    simulation.AddTeamMember(Johnny, new TeamMemberProperties { Role = Developer });
    simulation.AddTeamMember(Zenek, new TeamMemberProperties { Role = QA });

    simulation.Run();

    AssertLog(simulation, [
      Assigned(1, Andy, Developer, CodeX),
      Assigned(1, Johnny, Developer, CodeY),
      "Day 1: Developer Andy is working on task Code X",
      "Day 1: Developer Johnny is working on task Code Y",
      "Day 1: QA Zenek has nothing to work on",
      "Day 2: Developer Andy is working on task Code X",
      "Day 2: Developer Johnny is working on task Code Y",
      "Day 2: QA Zenek has nothing to work on",
      "Day 3: Developer Andy is working on task Code X",
      "Day 3: Developer Johnny is working on task Code Y",
      "Day 3: QA Zenek has nothing to work on",
      "Day 3: Developer Andy completed the task Code X",
      "Day 3: Developer Johnny completed the task Code Y",
      "Day 4: Developer Andy was assigned to task Code Z",
      "Day 4: QA Zenek was assigned to task Test X",
      "Day 4: Developer Andy is working on task Code Z",
      "Day 4: Developer Johnny has nothing to work on",
      "Day 4: QA Zenek is working on task Test X",
      "Day 4: QA Zenek completed the task Test X",
      "Day 5: QA Zenek was assigned to task Test Y",
      "Day 5: Developer Andy is working on task Code Z",
      "Day 5: Developer Johnny has nothing to work on",
      "Day 5: QA Zenek is working on task Test Y",
      "Day 5: QA Zenek completed the task Test Y",
      "Day 6: Developer Andy is working on task Code Z",
      "Day 6: Developer Johnny has nothing to work on",
      "Day 6: QA Zenek has nothing to work on",
      "Day 6: Developer Andy completed the task Code Z",
      "Day 7: QA Zenek was assigned to task Test Z",
      "Day 7: Developer Andy has nothing to work on",
      "Day 7: Developer Johnny has nothing to work on",
      "Day 7: QA Zenek is working on task Test Z",
      "Day 7: QA Zenek completed the task Test Z",
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

    simulation.AddTeamMember(Andy, new TeamMemberProperties
    {
      Role = Developer
    });
    simulation.AddTeamMember(Zenek, new TeamMemberProperties
    {
      Role = QA
    });


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