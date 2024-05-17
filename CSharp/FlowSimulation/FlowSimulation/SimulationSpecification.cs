using FluentAssertions;

namespace FlowSimulation;

[TestFixture(TestOf = typeof(Simulation))]
public class SimulationSpecification
{
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
        simulation.AddTeamMember("Andy");
            
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
        simulation.AddTeamMember("Andy");
            
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
        simulation.AddTeamMember("Andy");
            
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
        simulation.AddTeamMember("Andy");
        simulation.AddTeamMember("Johnny");
            
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
        simulation.AddTeamMember("Andy");
        FluentActions.Invoking(() => simulation.AddTeamMember("Andy"))
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
        simulation.AddTeamMember("Johnny");
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
        simulation.AddTeamMember("Johnny");
        simulation.AddWorkItem("X", DependingOn("Y"));
        simulation.AddWorkItem("Y", DependingOn("Z"));
        simulation.AddWorkItem("Z", DependingOn("X"));
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

        simulation.AddTeamMember("Andy");

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

        simulation.AddTeamMember("Andy");

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
    public void ShouldMakeMultipleDevelopersWorkOnDependentTasksAfterDependenciesAreFinished()
    {
        var simulation = new Simulation();

        simulation.AddWorkItem("X", DependingOn("Z"));
        simulation.AddWorkItem("Y", DependingOn("Z"));
        simulation.AddWorkItem("Z");

        simulation.AddTeamMember("Andy");
        simulation.AddTeamMember("Johnny");

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
    public void ShouldSupportMultipleDependencies()
    {
        var simulation = new Simulation();

        simulation.AddWorkItem("X", new WorkItemProperties { Dependencies = ["Y", "Z"] });
        simulation.AddWorkItem("Y", new WorkItemProperties());
        simulation.AddWorkItem("Z", new WorkItemProperties());

        simulation.AddTeamMember("Andy");
        simulation.AddTeamMember("Johnny");

        simulation.Run();

        AssertLog(simulation, [
                "Day 1: Developer Andy was assigned to task Y",
                "Day 1: Developer Johnny was assigned to task Z",
                "Day 1: Developer Andy is working on task Y",
                "Day 1: Developer Johnny is working on task Z",
                "Day 1: Developer Andy completed the task Y",
                "Day 1: Developer Johnny completed the task Z",
                "Day 2: Developer Andy was assigned to task X",
                "Day 2: Developer Andy is working on task X",
                "Day 2: Developer Johnny has nothing to work on",
                "Day 2: Developer Andy completed the task X",
            ]
        );
    }

    //BUG: assert all dependency ids point to existing items
    //BUG: dependencies with multiple developers
    private static void AssertLog(Simulation simulation, string[] entries)
    {
        simulation.Events.Entries.Should().Equal(entries);
    }
    private static WorkItemProperties DependingOn(string taskName)
    {
        return new WorkItemProperties
        {
            Dependencies = [taskName]
        };
    }
    private static WorkItemProperties WithPoints(int points)
    {
        return new WorkItemProperties { Points = points };
    }

    //bug add handovers (e.g. developer is QA or programmer)
}