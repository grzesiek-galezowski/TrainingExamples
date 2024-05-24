namespace FlowSimulation;

public interface IAssignmentContext
{
  void PursueExisting(string memberId, string role, IBacklogPart assignedItem);
  void TransitionTo(IAssignmentState newState);
  void SlackOff(string memberId, string role);
  void CloseAssignment(string memberId, string role, IBacklogPart assignedItem);
}

public class Assignment(Events events) : IAssignmentContext
{
  private IAssignmentState currentState = new UnassignedState();

  public void CloseIfNoWorkLeft(string memberId, string role)
  {
    currentState.CloseIfNoWorkLeft(this, memberId, role);
  }

  public bool CanBeWorkedOn()
  {
    return currentState.CanBeWorkedOn();
  }

  public void BeginOn(IBacklogPart newItem)
  {
    currentState.BeginOn(this, newItem);
  }

  public void Pursue(string role, string memberId)
  {
    currentState.Pursue(this, role, memberId);
  }

  void IAssignmentContext.PursueExisting(string memberId, string role, IBacklogPart assignedItem)
  {
    events.ReportItemInProgress(assignedItem, memberId, role);
    assignedItem.Progress();
  }

  void IAssignmentContext.TransitionTo(IAssignmentState newState)
  {
    currentState = newState;
    currentState.OnEnter();
  }

  void IAssignmentContext.SlackOff(string memberId, string role)
  {
    events.ReportSlack(memberId, role);
  }

  void IAssignmentContext.CloseAssignment(string memberId, string role, IBacklogPart assignedItem)
  {
    events.ReportItemCompleted(assignedItem, memberId, role);
  }
}