namespace FlowSimulation;

public class Simulation
{
    private readonly Team team;
    private readonly Backlog backlog;

    public Simulation()
    {
        Events = new Events();
        team = new Team();
        backlog = new Backlog();
    }

    public Events Events { get; }

    public void Run()
    {
        if(backlog.IsEmpty())
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
        //bug e.g. qa task but no qa member: backlog.AssertCanBeAchievedBy(team);
        while (backlog.IsNotCompleted())
        {
            backlog.AssignItemsTo(team);
            team.WorkOnAssignedItems();
            Events.MoveToNextDay();
        }
    }

    public void AddWorkItem(string itemId) //BUG: get rid of it later?
    {
        AddWorkItem(itemId, new WorkItemProperties());
    }

    //bug extract property object
    public void AddWorkItem(string itemId, WorkItemProperties workItemProperties)
    {
        backlog.AssertDoesNotAlreadyContain(itemId);
        backlog.Add(WorkItem.BasedOn(itemId, workItemProperties));
    }

    public void AddTeamMember(string teamMemberId)
    {
        AddTeamMember(teamMemberId, new TeamMemberProperties());
    }

    private void AddTeamMember(string teamMemberId, TeamMemberProperties teamMemberProperties)
    {
        team.AssertDoesNotAlreadyHaveMemberWith(teamMemberId);
        team.Add(new TeamMember(teamMemberId, teamMemberProperties.Role, Events));
    }
}