namespace FlowSimulation;

public class Assignment(Events events)
{
    private WorkItem? item; //bug nullable

    public void PursueExisting(string memberId, string role)
    {
        events.ReportItemInProgress(item, memberId, role);
        item.Progress();
    }

    public void CloseIfNoWorkLeft(string memberId, string role)
    {
        if (item != null && item.IsCompleted()) //bug change to state machine
        {
            events.ReportItemCompleted(item, memberId, role);
            item = null;
        }
    }

    public void PursueSlack(string memberId, string role)
    {
        events.ReportSlack(memberId, role);
    }

    public bool NeedsWork()
    {
        return item != null;
    }

    public void BeginOn(WorkItem newItem)
    {
        item = newItem;
        item.ChangeStatusToAssigned();
    }
}
