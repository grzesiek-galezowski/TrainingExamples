namespace FlowSimulation;

public class AssignedState(WorkItem assignedItem) : IAssignmentState
{
  public void BeginOn(IAssignmentContext assignment, WorkItem newItem)
  {
    //bug throw an exception?
  }

  public void CloseIfNoWorkLeft(IAssignmentContext assignment, TeamMemberId memberId, RoleId role)
  {
    if (assignedItem.IsCompleted()) //bug change to state machine
    {
      assignment.CloseAssignment(memberId, role, assignedItem);
      assignment.TransitionTo(new UnassignedState());
    }
  }

  public void Pursue(IAssignmentContext assignment, RoleId role, TeamMemberId memberId)
  {
    assignment.PursueExisting(memberId, role, assignedItem); //BUG: move the arguments to constructor of assignment
  }

  public bool CanBeWorkedOn()
  {
    return true;
  }

  public void OnEnter()
  {
    assignedItem.ChangeStatusToAssigned();
  }
}