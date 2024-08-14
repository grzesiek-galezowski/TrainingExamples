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

#pragma warning disable CS9124 // Parameter is captured into the state of the enclosing type and its value is also used to initialize a field, property, or event.
  private int currentPoints = points;
#pragma warning restore CS9124 // Parameter is captured into the state of the enclosing type and its value is also used to initialize a field, property, or event.
  private bool assigned;
  private ImmutableList<ItemGroup> parents = [];

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

  public bool HasNoPendingDependencies(WorkItemsRepository workItemsRepository)
  {
    var dependencies = workItemsRepository.FindItemsBy(dependencyNames);
    return dependencies.TrueForAll(item => item.IsCompleted());
  }

  public static WorkItem BasedOn(ItemId itemId, WorkItemProperties workItemProperties)
  {
    return new WorkItem(itemId,
      workItemProperties.Points,
      workItemProperties.Priority,
      workItemProperties.Dependencies,
      workItemProperties.MaybeRequiredRole);
  }

  public void AssertDoesNotHaveHigherPriorityThanAnyOfItsDependencies(WorkItemsRepository workItemsRepository)
  {
    var dependencies = workItemsRepository.FindItemsBy(dependencyNames);
    foreach (var dependency in dependencies)
    {
      AssertDoesNotHaveLowerPriorityThan(dependency);
    }
  }

  public void AssertDoesNotDependOnItself(WorkItemsRepository workItemsRepository)
  {
    var dependencies = workItemsRepository.FindItemsBy(dependencyNames);
    foreach (var dependency in dependencies)
    {
      dependency.AssertDoesNotDependOn([id], workItemsRepository);
    }
  }
  
  public void AssertAllDependenciesExist(WorkItemsRepository workItemsRepository)
  {
    foreach (var dependencyName in dependencyNames)
    {
      if (!workItemsRepository.Contains(dependencyName))
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

  private bool HasPriorityAtMost(int testedPriority)
  {
    return Priority > testedPriority;
  }

  private void AssertDoesNotHaveLowerPriorityThan(WorkItem dependency)
  {
    if (HasPriorityAtMost(dependency.Priority))
    {
      throw new Exception($"{this} has lower dependency than its dependencies");
    }
  }

  private void AssertDoesNotDependOn(ImmutableList<ItemId> alreadyEncounteredIds, WorkItemsRepository workItemsRepository)
  {
    AssertIdIsNoneOf(alreadyEncounteredIds);
    var dependencies = workItemsRepository.FindItemsBy(dependencyNames);
    foreach (var dependency in dependencies)
    {
      dependency.AssertDoesNotDependOn(alreadyEncounteredIds.Add(id), workItemsRepository);
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
    parents = parents.Add(itemGroup);
  }

  public void Close(IEventsDestination events, TeamMemberId memberId, RoleId role)
  {
    events.ReportItemCompleted(id, memberId, role);
    foreach (var itemGroup in parents)
    {
      itemGroup.NotifyChildCompleted(id, points);
    }
  }
}