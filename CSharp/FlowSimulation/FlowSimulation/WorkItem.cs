using System.Collections.Immutable;

namespace FlowSimulation;

public class WorkItem(string id, int points, int priority, ImmutableList<string> dependencyNames)
{
    private readonly ImmutableList<string> dependencyNames = dependencyNames;
    public override string ToString() => id;
    private bool assigned;

    public void Progress()
    {
        points--;
        //bug throw if pointsLeft < 0
    }

    public bool IsCompleted()
    {
        return points == 0;
    }

    public bool IsAssigned()
    {
        return assigned;
    }

    public void ChangeStatusToAssigned()
    {
        assigned = true;
    }

    public bool HasName(string itemId)
    {
        return id == itemId;
    }

    public int Priority => priority;

    public bool HasNoPendingDependencies(List<WorkItem> workItems)
    {
        var dependencies = SelectMyDependenciesFrom(workItems);
        return AreAllCompleted(dependencies);
    }

    private static bool AreAllCompleted(IEnumerable<WorkItem> dependencies)
    {
        return dependencies.All(item => item.IsCompleted());
    }

    private ImmutableList<WorkItem> SelectMyDependenciesFrom(List<WorkItem> workItems)
    {
        return workItems.Where(i => dependencyNames.Exists(i.HasName))
            .ToImmutableList();
    }

    public static WorkItem BasedOn(string itemId, WorkItemProperties workItemProperties)
    {
        return new WorkItem(itemId,
            workItemProperties.Points,
            workItemProperties.Priority,
            workItemProperties.Dependencies);
    }

    private bool HasHigherPriorityThan(WorkItem workItem)
    {
        return workItem.HasPriorityAtMost(Priority);
    }

    private bool HasPriorityAtMost(int priority)
    {
        return Priority > priority; //lower is higher
    }

    private void AssertDoesNotHaveLowerPriorityThan(WorkItem dependency)
    {
        if (dependency.HasHigherPriorityThan(this))
        {
            throw new Exception($"{this} has lower dependency than its dependencies");
        }
    }

    public void AssertDoesNotHaveHigherPriorityThanAnyOfItsDependencies(List<WorkItem> list)
    {
        var dependencies = SelectMyDependenciesFrom(list);
        foreach (var dependency in dependencies)
        {
            AssertDoesNotHaveLowerPriorityThan(dependency);
        }
    }

    public void AssertDoesNotDependOnItself(List<WorkItem> workItems)
    {
        var dependencies = SelectMyDependenciesFrom(workItems);
        foreach (var dependency in dependencies)
        {
            dependency.AssertDoesNotDependOn([id], workItems);
        }
    }

    private void AssertDoesNotDependOn(ImmutableList<string> alreadyEncounteredIds, List<WorkItem> workItems)
    {
        AssertIdIsNoneOf(alreadyEncounteredIds);
        var dependencies = SelectMyDependenciesFrom(workItems);
        foreach (var dependency in dependencies)
        {
            dependency.AssertDoesNotDependOn(alreadyEncounteredIds.Add(id), workItems);
        }
    }

    private void AssertIdIsNoneOf(ImmutableList<string> alreadyEncounteredIds)
    {
        foreach (var alreadyEncounteredId in alreadyEncounteredIds)
        {
            if (alreadyEncounteredId == id)
            {
                throw new Exception("Circular dependency for " + id);
            }
        }
    }
}