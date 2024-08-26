using System.Collections.Immutable;
using FlowSimulation.ProductionCode;
using NSubstitute;

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
    simulation.TextLog.AssertConsistsOf([ExpectedEvents.NoItemsOnTheBacklog()]);
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

  public void AssertConsistsOf(ExpectedEvent[] messages)
  {
    simulation.TextLog.AssertConsistsOf(messages);
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