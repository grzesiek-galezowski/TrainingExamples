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

  private List<WorkItem> PrioritizedWorkItems()
  {
    return workItemsList.AllItems() //bug
      .Where(w => w.HasNoPendingDependencies(workItemsList))
      .OrderBy(w => w.Priority).ToList();
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

  public void Add(WorkItem workItem)
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