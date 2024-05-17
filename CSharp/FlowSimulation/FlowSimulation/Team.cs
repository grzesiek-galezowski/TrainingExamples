namespace FlowSimulation;

public class Team
{
    private readonly List<TeamMember> developers = [];

    public bool HasNoMembers()
    {
        return developers.Count == 0;
    }

    public void AssignWork(List<WorkItem> workItems)
    {
        foreach (var dev in developers.Where(d => !d.HasWork))
        {
            var unassignedWorkItem = workItems.Find(i => !i.IsAssigned());
            if (unassignedWorkItem != null)
            {
                dev.Assign(unassignedWorkItem);
            }
        }
    }

    public void WorkOnAssignedItems()
    {
        foreach (var dev in developers)
        {
            dev.WorkOnAssignedItem();
        }
        foreach (var dev in developers)
        {
            dev.CompleteDoneItems();
        }
    }


    public void AssertDoesNotAlreadyHaveMemberWith(string developerId)
    {
        if (HasDeveloperWith(developerId))
        {
            throw new Exception("Duplicate developer");
        }
    }

    public void Add(TeamMember teamMember)
    {
        developers.Add(teamMember);
    }

    private bool HasDeveloperWith(string developerId)
    {
        return developers.Exists(d => d.Has(developerId));
    }
}