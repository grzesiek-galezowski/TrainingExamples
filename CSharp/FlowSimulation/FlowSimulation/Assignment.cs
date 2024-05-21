using Core.Maybe;

namespace FlowSimulation;

public class Assignment(Events events)
{
  private Maybe<WorkItem> item; //bug nullable

  public void PursueExisting(string memberId, string role)
  {
    events.ReportItemInProgress(item.Value(), memberId, role);
    item.Value().Progress();
  }

  public void CloseIfNoWorkLeft(string memberId, string role)
  {
    if (item.IsSomething() && item.Value().IsCompleted()) //bug change to state machine
    {
      events.ReportItemCompleted(item.Value(), memberId, role);
      item = Maybe<WorkItem>.Nothing;
    }
  }

  public void PursueSlack(string memberId, string role)
  {
    events.ReportSlack(memberId, role);
  }

  public bool NeedsWork()
  {
    return item.IsSomething();
  }

  public void BeginOn(WorkItem newItem)
  {
    item = newItem.Just();
    item.Value().ChangeStatusToAssigned();
  }
}