using System.Collections.Immutable;

namespace FlowSimulation.ProductionCode;

public class Backlog
{
  private readonly WorkItemsRepository workItemsRepository;

  public Backlog()
  {
    workItemsRepository = new WorkItemsRepository([]);
  }

  public bool IsEmpty()
  {
    return workItemsRepository.IsEmpty();
  }

  public bool IsNotCompleted()
  {
    return !workItemsRepository.FindNotCompleted().IsEmpty;
  }

  public void AssignItemsTo(Team team)
  {
    team.AssignWork(PrioritizedWorkItems());
  }

  private ImmutableList<WorkItem> PrioritizedWorkItems()
  {
    return workItemsRepository.AllItems() //bug
      .Where(w => w.HasNoPendingDependencies(workItemsRepository))
      .OrderBy(w => w.Priority).ToImmutableList();
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
    foreach (var workItem in workItemsRepository.AllItems())
    {
      workItem.AssertDoesNotDependOnItself(workItemsRepository);
      workItem.AssertAllDependenciesExist(workItemsRepository);
      workItem.AssertDoesNotHaveHigherPriorityThanAnyOfItsDependencies(workItemsRepository);
    }
  }

  public void AssertRequiresOnlyRolesAvailableInThe(Team team)
  {
    foreach (var workItem in workItemsRepository.AllItems())
    {
      workItem.AssertRequiresRoleAvailableInThe(team);
    }
  }

  public void Add(ItemGroup itemGroup)
  {
    itemGroup.AddAsParentToItsChildrenIn(workItemsRepository);
  }

  public void Add(WorkItem workItem)
  {
    workItemsRepository.Add(workItem);
  }

  private bool HasItemWith(ItemId itemId)
  {
    return workItemsRepository.FindByItemId(itemId).HasValue;
  }
  //bug change many lists to hashsets
}