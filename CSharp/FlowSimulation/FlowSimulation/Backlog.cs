using System.Collections.Immutable;

namespace FlowSimulation;

public class Backlog
{
  private readonly WorkItemsList workItemsList;

  public Backlog()
  {
    workItemsList = new WorkItemsList([]);
  }

  public bool IsEmpty()
  {
    return workItemsList.IsEmpty();
  }

  public bool IsNotCompleted()
  {
    return workItemsList.FindNotCompleted().Any();
  }

  public void AssignItemsTo(Team team)
  {
    team.AssignWork(PrioritizedWorkItems());
  }

  private WorkItemsList PrioritizedWorkItems()
  {
    return new WorkItemsList(workItemsList.AllItems() //bug
      .Where(w => w.HasNoPendingDependencies(workItemsList))
      .OrderBy(w => w, new WorkItemPriorityComparer()).ToList());
  }

  private bool HasItemWith(string itemId)
  {
    return workItemsList.FindByItemId(itemId).Any();
  }

  public void AssertDoesNotAlreadyContain(string itemId)
  {
    if (HasItemWith(itemId))
    {
      throw new Exception("Duplicate work item");
    }
  }

  public void Add(IBacklogPart workItem)
  {
    workItemsList.Add(workItem);
  }

  public void AssertIsCoherent()
  {
    foreach (var workItem in workItemsList.AllItems())
    {
      workItem.AssertDoesNotDependOnItself(workItemsList);
      workItem.AssertAllDependenciesExist(workItemsList);
      workItem.AssertDoesNotHaveHigherPriorityThanAnyOfItsDependencies(workItemsList);
    }
  }

  public void AssertRequiresOnlyRolesAvailableInThe(Team team)
  {
    foreach (var workItem in workItemsList.AllItems())
    {
      workItem.AssertRequiresRoleAvailableInThe(team);
    }
  }
}

internal class WorkItemPriorityComparer : IComparer<IBacklogPart>
{
  public int Compare(IBacklogPart? x, IBacklogPart? y)
  {
    if (ReferenceEquals(x, y)) return 0;
    if (y is null) return 1;
    if (x is null) return -1;
    if(x.IsLessCriticalThan(y)) return 1;
    if(x.IsMoreCriticalThan(y)) return -1;
    return 0;
  }
}