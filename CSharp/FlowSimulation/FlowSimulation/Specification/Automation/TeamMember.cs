using FlowSimulation.ProductionCode;

namespace FlowSimulation.Specification.Automation;

public class TeamMember(TeamMemberId id, string role, IEventsDestination events)
{
  private readonly Assignment assignment = new(events);

  public override string ToString()
  {
    return id.ToString();
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

  public bool Has(TeamMemberId developerId)
  {
    return id == developerId;
  }

  public void CompleteDoneItems()
  {
    assignment.CloseIfNoWorkLeft(id, role);
  }

  public bool HasRole(RoleId roleName)
  {
    return role == roleName;
  }

  public bool CanWorkOn(WorkItem workItem)
  {
    return workItem.IsForRole(role);
  }
}