namespace FlowSimulation;

internal class UnassignedState : IAssignmentState //bug move
{
  public void BeginOn(IAssignmentContext assignment, WorkItem newItem)
  {
    assignment.Assign(newItem);
    assignment.TransitionTo(new AssignedState());
  }

  public void CloseIfNoWorkLeft(IAssignmentContext assignment, string memberId, string role)
  {
  }

  public void Pursue(IAssignmentContext assignment, string role, string memberId)
  {
    assignment.SlackOff(memberId, role);
  }
}