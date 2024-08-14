using System.Collections.Immutable;

namespace FlowSimulation;

public class Simulation
{
  private readonly Team team = new();
  private readonly Backlog backlog = new();
  public TextLog TextLog { get; } = new();
  private readonly IEventsDestination eventDestination;

  public Simulation(params IEventsDestination[] additionalEventDestinations)
  {
    eventDestination = new CompoundEvents(additionalEventDestinations.ToImmutableArray().Add(TextLog));
  }

  public void Run()
  {
    if (backlog.IsEmpty())
    {
      eventDestination.NoItemsOnTheBacklog();
    }
    else if (team.HasNoMembers())
    {
      eventDestination.NoMembersOnTheTeam();
    }
    else
    {
      RunLoop(eventDestination);
    }
  }

  private void RunLoop(IEventsDestination eventsDestination)
  {
    backlog.AssertIsCoherent();
    backlog.AssertRequiresOnlyRolesAvailableInThe(team);

    while (backlog.IsNotCompleted())
    {
      backlog.AssignItemsTo(team);
      team.WorkOnAssignedItems();
      eventsDestination.NextDay();
    }
  }

  public void AddWorkItem(ItemId itemId) //BUG: get rid of it later?
  {
    AddWorkItem(itemId, new WorkItemProperties());
  }

  //bug extract property object
  public void AddWorkItem(ItemId itemId, WorkItemProperties workItemProperties)
  {
    backlog.AssertDoesNotAlreadyContain(itemId);
    backlog.Add(WorkItem.BasedOn(itemId, workItemProperties));
  }

  public void AddTeamMember(TeamMemberId teamMemberId)
  {
    AddTeamMember(teamMemberId, new TeamMemberProperties());
  }

  public void AddTeamMember(TeamMemberId teamMemberId, TeamMemberProperties properties)
  {
    team.AssertDoesNotAlreadyHaveMemberWith(teamMemberId);
    team.Add(new TeamMember(teamMemberId, properties.Role, eventDestination));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="itemGroupId"></param>
  /// <param name="groupedItemsIds"></param>
  public void AddWorkItemGroup(ItemId itemGroupId, ImmutableList<ItemId> groupedItemsIds)
  {
    backlog.Add(new ItemGroup(itemGroupId, groupedItemsIds, eventDestination));
  }
}