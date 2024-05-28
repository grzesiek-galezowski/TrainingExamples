using System.Collections.Immutable;
using Core.Maybe;

namespace FlowSimulation;

public class WorkItem(
  ItemId id,
  int points,
  int priority,
  ImmutableList<ItemId> dependencyNames,
  Maybe<RoleId> requiredRole)
{
  public override string ToString() => id.Text;

  private int currentPoints = points;
  private bool assigned;
  private readonly List<ItemGroup> parents = [];

  public void Progress()
  {
    currentPoints--;
    //bug throw if pointsLeft < 0
  }

  public bool IsCompleted()
  {
    return currentPoints == 0;
  }

  public bool IsAssigned()
  {
    return assigned;
  }

  public void ChangeStatusToAssigned()
  {
    assigned = true;
  }

  public bool Has(ItemId itemId)
  {
    return id == itemId;
  }

  public int Priority => priority;

  public bool HasNoPendingDependencies(WorkItemsList workItemsList)
  {
    var dependencies = workItemsList.FindItemsBy(dependencyNames);
    return workItemsList.AreAllCompleted(dependencies);
  }

  public static WorkItem BasedOn(ItemId itemId, WorkItemProperties workItemProperties)
  {
    return new WorkItem(itemId,
      workItemProperties.Points,
      workItemProperties.Priority,
      workItemProperties.Dependencies,
      workItemProperties.MaybeRequiredRole);
  }

  public void AssertDoesNotHaveHigherPriorityThanAnyOfItsDependencies(WorkItemsList workItemsList)
  {
    var dependencies = workItemsList.FindItemsBy(dependencyNames);
    foreach (var dependency in dependencies)
    {
      AssertDoesNotHaveLowerPriorityThan(dependency);
    }
  }

  public void AssertDoesNotDependOnItself(WorkItemsList workItemsList)
  {
    var dependencies = workItemsList.FindItemsBy(dependencyNames);
    foreach (var dependency in dependencies)
    {
      dependency.AssertDoesNotDependOn([id], workItemsList);
    }
  }
  
  public void AssertAllDependenciesExist(WorkItemsList workItemsList)
  {
    foreach (var dependencyName in dependencyNames)
    {
      if (!workItemsList.Contains(dependencyName))
      {
        throw new Exception($"work item {id} depends on {dependencyName} which does not exist");
      }
    }
  }

  public void AssertRequiresRoleAvailableInThe(Team team)
  {
    requiredRole.Do(team.AssertHasSomeoneWithRole);
  }

  public bool IsForRole(string role)
  {
    return requiredRole.Select(r => r == role).OrTrue();
  }

  private bool HasPriorityAtMost(int priority)
  {
    return Priority > priority; //lower is higher
  }

  private void AssertDoesNotHaveLowerPriorityThan(WorkItem dependency)
  {
    if (HasPriorityAtMost(dependency.Priority))
    {
      throw new Exception($"{this} has lower dependency than its dependencies");
    }
  }

  private void AssertDoesNotDependOn(ImmutableList<ItemId> alreadyEncounteredIds, WorkItemsList workItemsList)
  {
    AssertIdIsNoneOf(alreadyEncounteredIds);
    var dependencies = workItemsList.FindItemsBy(dependencyNames);
    foreach (var dependency in dependencies)
    {
      dependency.AssertDoesNotDependOn(alreadyEncounteredIds.Add(id), workItemsList);
    }
  }

  private void AssertIdIsNoneOf(ImmutableList<ItemId> alreadyEncounteredIds)
  {
    foreach (var alreadyEncounteredId in alreadyEncounteredIds)
    {
      if (alreadyEncounteredId == id)
      {
        throw new Exception("Circular dependency for " + id);
      }
    }
  }

  public void AddParent(ItemGroup itemGroup)
  {
    parents.Add(itemGroup);
  }

  public void Close(Events events, TeamMemberId memberId, RoleId role)
  {
    events.ReportItemCompleted(id, memberId, role);
    foreach (var itemGroup in parents)
    {
      itemGroup.NotifyChildCompleted(id, points);
    }
  }
}