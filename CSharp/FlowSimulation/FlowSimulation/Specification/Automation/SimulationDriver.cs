using System.Collections.Immutable;
using FlowSimulation.ProductionCode;
using NSubstitute;
using TddXt.XNSubstitute;

namespace FlowSimulation.Specification.Automation;

public class SimulationDriver
{
  private readonly Simulation simulation;
  private readonly IEventsDestination additionalDestination;

  public SimulationDriver()
  {
    additionalDestination = Substitute.For<IEventsDestination>();
    simulation = new Simulation(additionalDestination);
  }

  public void RunSimulation() => simulation.Run();

  public void ShouldReportNoItemsInTheBacklog()
  {
    simulation.TextLog.AssertConsistsOf([ExpectedEvents.NoItemsOnTheBacklog().Text]);
    additionalDestination.Received(1).NoItemsOnTheBacklog();
  }

  public void AddWorkItem(ItemId itemId)
  {
    simulation.AddWorkItem(itemId);
  }

  public void AddTeamMember(TeamMemberId teamMemberId)
  {
    simulation.AddTeamMember(teamMemberId);
  }

  internal void AddTeamMember(TeamMemberId teamMemberId, TeamMemberProperties teamMemberProperties)
  {
    simulation.AddTeamMember(teamMemberId, teamMemberProperties);
  }

  public void AssertTextLogConsistsOf(ExpectedEvent[] expectedEvents)
  {
    simulation.TextLog.AssertConsistsOf([.. expectedEvents.Select(m => m.Text)]);
  }

  public void AssertEvents(ExpectedEvent[] expectedEvents)
  {
    XReceived.Exactly(() =>
    {
      foreach (var expectedEvent in expectedEvents)
      {
        expectedEvent.CheckAgainst(additionalDestination);
      }
    });
  }

  public void AddWorkItem(string itemId, WorkItemProperties properties)
  {
    simulation.AddWorkItem(itemId, properties);
  }

  public void AddWorkItemGroup(string itemGroupId, ImmutableList<ItemId> dependencies)
  {
    simulation.AddWorkItemGroup(itemGroupId, dependencies);
  }
}