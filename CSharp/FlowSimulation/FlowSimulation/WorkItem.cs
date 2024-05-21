using System.Collections.Immutable;
using Core.Maybe;

namespace FlowSimulation;

public class WorkItem(
  string id,
  int points,
  int priority,
  ImmutableList<string> dependencyNames,
  Maybe<string> requiredRole)
{
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

  public bool HasNoPendingDependencies(WorkItemsList workItemsList)
  {
    var dependencies = workItemsList.FindItemsBy(dependencyNames);
    return workItemsList.AreAllCompleted(dependencies);
  }

  public static WorkItem BasedOn(string itemId, WorkItemProperties workItemProperties)
  {
    return new WorkItem(itemId,
        workItemProperties.Points,
        workItemProperties.Priority,
        workItemProperties.Dependencies,
        workItemProperties.RequiredRole);
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

  private void AssertDoesNotDependOn(ImmutableList<string> alreadyEncounteredIds, WorkItemsList workItemsList)
  {
    AssertIdIsNoneOf(alreadyEncounteredIds);
    var dependencies = workItemsList.FindItemsBy(dependencyNames);
    foreach (var dependency in dependencies)
    {
      dependency.AssertDoesNotDependOn(alreadyEncounteredIds.Add(id), workItemsList);
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
}