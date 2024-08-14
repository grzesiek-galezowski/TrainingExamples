namespace FlowSimulation;

internal class UnassignedState : IAssignmentState //bug move
{
  public void BeginOn(IAssignmentContext assignment, WorkItem newItem)
  {
    assignment.TransitionTo(new AssignedState(newItem));
  }

  public void CloseIfNoWorkLeft(IAssignmentContext assignment, TeamMemberId memberId, RoleId role)
  {
  }

  public void Pursue(IAssignmentContext assignment, RoleId role, TeamMemberId memberId)
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