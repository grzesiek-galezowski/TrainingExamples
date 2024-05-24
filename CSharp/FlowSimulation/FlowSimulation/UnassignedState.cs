namespace FlowSimulation;

internal class UnassignedState : IAssignmentState //bug move
{
  public void BeginOn(IAssignmentContext assignment, IBacklogPart newItem)
  {
    assignment.TransitionTo(new AssignedState(newItem));
  }

  public void CloseIfNoWorkLeft(IAssignmentContext assignment, string memberId, string role)
  {
  }

  public void Pursue(IAssignmentContext assignment, string role, string memberId)
  {
    assignment.SlackOff(memberId, role);
  }

  public bool CanBeWorkedOn()
  {
    return false;
  }

  public void OnEnter()
  {
  }
}