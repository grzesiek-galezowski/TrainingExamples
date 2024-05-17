namespace FlowSimulation;

public class Backlog
{
    private readonly List<WorkItem> workItems = [];

    public bool IsEmpty()
    {
        return workItems.Count == 0;
    }

    public bool IsNotCompleted()
    {
        return workItems.Exists(i => !i.IsCompleted());
    }

    public void AssignItemsTo(Team team)
    {
        team.AssignWork(PrioritizedWorkItems());
    }

    private List<WorkItem> PrioritizedWorkItems()
    {
        return workItems
            .Where(w => w.HasNoPendingDependencies(workItems))
            .OrderBy(w => w.Priority).ToList();
    }

    private bool HasItemWith(string itemId)
    {
        return workItems.Exists(i => i.HasName(itemId));
    }

    public void AssertDoesNotAlreadyContain(string itemId)
    {
        if(HasItemWith(itemId))
        {
            throw new Exception("Duplicate work item");
        }
    }

    public void Add(WorkItem workItem)
    {
        workItems.Add(workItem);
    }

    public void AssertIsCoherent()
    {
        foreach (var workItem in workItems)
        {
            workItem.AssertDoesNotDependOnItself(workItems);
            workItem.AssertDoesNotHaveHigherPriorityThanAnyOfItsDependencies(workItems);
        }
    }
}