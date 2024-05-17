namespace FlowSimulation;

public class TeamMember(string id, string role, Events events)
{
    private Assignment assignment = new Assignment(events);

    public override string ToString()
    {
        return id;
    }

    public bool HasWork => assignment.NeedsWork();

    public void Assign(WorkItem item)
    {
        events.ReportAssignment(id, item, role);
        assignment.BeginOn(item);
    }

    public void WorkOnAssignedItem()
    {
        if (assignment.NeedsWork())
        {
            assignment.PursueExisting(id, role); //BUG: move the arguments to constructor of assignment
        }
        else
        {
            assignment.PursueSlack(id, role);
        }
    }

    public bool Has(string developerId)
    {
        return id == developerId;
    }

    public void CompleteDoneItems()
    {
        assignment.CloseIfNoWorkLeft(id, role);
    }
}