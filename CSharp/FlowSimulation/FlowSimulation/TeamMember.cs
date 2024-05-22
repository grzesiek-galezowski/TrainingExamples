namespace FlowSimulation;

public class TeamMember(string id, string role, Events events)
{
  private readonly Assignment assignment = new(events);

  public override string ToString()
  {
    return id;
  }

  public bool HasWork => assignment.CanBeWorkedOn();

  public void Assign(WorkItem item)
  {
    events.ReportAssignment(id, item, role);
    assignment.BeginOn(item);
  }

  public void WorkOnAssignedItem()
  {
    assignment.Pursue(role, id);
  }

  public bool Has(string developerId)
  {
    return id == developerId;
  }

  public void CompleteDoneItems()
  {
    assignment.CloseIfNoWorkLeft(id, role);
  }

  public bool HasRole(string roleName)
  {
    return role == roleName;
  }

  public bool CanWorkOn(WorkItem workItem)
  {
    return workItem.IsForRole(role);
  }
}