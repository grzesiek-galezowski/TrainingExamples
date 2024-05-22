namespace FlowSimulation;

public class AssignedState : IAssignmentState
{
  public void BeginOn(IAssignmentContext assignment, WorkItem newItem)
  {
    //bug throw an exception?
  }

  public void CloseIfNoWorkLeft(IAssignmentContext assignment, string memberId, string role)
  {
    if (assignment.IsWorkItemCompleted()) //bug change to state machine
    {
      assignment.CloseAssignment(memberId, role);
      assignment.TransitionTo(new UnassignedState());
    }
  }

  public void Pursue(IAssignmentContext assignment, string role, string memberId)
  {
    assignment.PursueExisting(memberId, role); //BUG: move the arguments to constructor of assignment
  }
}