using System.Collections.Immutable;

namespace FlowSimulation;

public class Backlog
{
  private readonly WorkItemsList workItemsList;
  private readonly List<ItemGroup> itemGroups = [];

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
    return !workItemsList.FindNotCompleted().IsEmpty;
  }

  public void AssignItemsTo(Team team)
  {
    team.AssignWork(PrioritizedWorkItems());
  }

  private ImmutableList<WorkItem> PrioritizedWorkItems()
  {
    return workItemsList.AllItems() //bug
      .Where(w => w.HasNoPendingDependencies(workItemsList))
      .OrderBy(w => w.Priority).ToImmutableList();
  }

  private bool HasItemWith(ItemId itemId)
  {
    return workItemsList.FindByItemId(itemId).HasValue;
  }

  public void AssertDoesNotAlreadyContain(ItemId itemId)
  {
    if (HasItemWith(itemId))
    {
      throw new Exception("Duplicate work item");
    }
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

  public void Add(ItemGroup workItem)
  {
    itemGroups.Add(workItem);
    workItem.AddAsParentToItsChildrenIn(workItemsList);
  }

  public void Add(WorkItem workItem)
  {
    workItemsList.Add(workItem);
  }

  //bug change many lists to hashsets
}