namespace FlowSimulation;

public class Simulation
{
  private readonly Team team = new();
  private readonly Backlog backlog = new();
  public Events Events { get; } = new();

  public void Run()
  {
    if (backlog.IsEmpty())
    {
      Events.NoItemsOnTheBacklog();
    }
    else if (team.HasNoMembers())
    {
      Events.NoMembersOnTheTeam();
    }
    else
    {
      RunLoop();
    }
  }

  private void RunLoop()
  {
    backlog.AssertIsCoherent();
    backlog.AssertRequiresOnlyRolesAvailableInThe(team);
    while (backlog.IsNotCompleted())
    {
      backlog.AssignItemsTo(team);
      team.WorkOnAssignedItems();
      Events.MoveToNextDay();
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

  public void AddTeamMember(string teamMemberId)
  {
    AddTeamMember(teamMemberId, new TeamMemberProperties());
  }

  public void AddTeamMember(string teamMemberId, TeamMemberProperties properties)
  {
    team.AssertDoesNotAlreadyHaveMemberWith(teamMemberId);
    team.Add(new TeamMember(teamMemberId, properties.Role, Events));
  }
}