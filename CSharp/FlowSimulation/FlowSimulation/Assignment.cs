namespace FlowSimulation;

public interface IAssignmentContext
{
  void PursueExisting(TeamMemberId memberId, RoleId role, WorkItem assignedItem);
  void TransitionTo(IAssignmentState newState);
  void SlackOff(TeamMemberId memberId, RoleId role);
  void CloseAssignment(TeamMemberId memberId, RoleId role, WorkItem assignedItem);
}

public class Assignment(Events events) : IAssignmentContext
{
  private IAssignmentState currentState = new UnassignedState();

  public void CloseIfNoWorkLeft(TeamMemberId memberId, string role)
  {
    currentState.CloseIfNoWorkLeft(this, memberId, role);
  }

  public bool CanBeWorkedOn()
  {
    return currentState.CanBeWorkedOn();
  }

  public void BeginOn(WorkItem newItem)
  {
    currentState.BeginOn(this, newItem);
  }

  public void Pursue(string role, TeamMemberId memberId)
  {
    currentState.Pursue(this, role, memberId);
  }

  void IAssignmentContext.PursueExisting(TeamMemberId memberId, RoleId role, WorkItem assignedItem)
  {
    events.ReportItemInProgress(assignedItem, memberId, role);
    assignedItem.Progress();
  }

  void IAssignmentContext.TransitionTo(IAssignmentState newState)
  {
    currentState = newState;
    currentState.OnEnter();
  }

  void IAssignmentContext.SlackOff(TeamMemberId memberId, RoleId role)
  {
    events.ReportSlack(memberId, role);
  }

  void IAssignmentContext.CloseAssignment(TeamMemberId memberId, RoleId role, WorkItem assignedItem)
  {
    assignedItem.Close(events, memberId, role);
  }
}