using Core.Maybe;

namespace FlowSimulation;

public interface IAssignmentContext
{
  void PursueExisting(string memberId, string role);
  void Assign(WorkItem newItem);
  void TransitionTo(IAssignmentState newState);
  void SlackOff(string memberId, string role);
  void CloseAssignment(string memberId, string role);
  bool IsWorkItemCompleted();
}

public class Assignment(Events events) : IAssignmentContext
{
  private Maybe<WorkItem> item; //bug nullable
  private IAssignmentState currentState = new UnassignedState();

  public void CloseIfNoWorkLeft(string memberId, string role)
  {
    currentState.CloseIfNoWorkLeft(this, memberId, role);
  }

  public bool CanBeWorkedOn()
  {
    return item.IsSomething();
  }

  public void BeginOn(WorkItem newItem)
  {
    currentState.BeginOn(this, newItem);
  }

  public void Pursue(string role, string memberId)
  {
    currentState.Pursue(this, role, memberId);
  }

  void IAssignmentContext.PursueExisting(string memberId, string role)
  {
    events.ReportItemInProgress(item.Value(), memberId, role);
    item.Value().Progress();
  }

  void IAssignmentContext.Assign(WorkItem newItem)
  {
    item = newItem.Just();
    item.Value().ChangeStatusToAssigned();
  }

  void IAssignmentContext.TransitionTo(IAssignmentState newState)
  {
    currentState = newState;
  }

  void IAssignmentContext.SlackOff(string memberId, string role)
  {
    events.ReportSlack(memberId, role);
  }

  void IAssignmentContext.CloseAssignment(string memberId, string role)
  {
    events.ReportItemCompleted(item.Value(), memberId, role);
    item = Maybe<WorkItem>.Nothing;
  }

  bool IAssignmentContext.IsWorkItemCompleted()
  {
    return item.Value().IsCompleted();
  }
}