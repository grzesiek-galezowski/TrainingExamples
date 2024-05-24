using System.Collections.Immutable;
using Core.Maybe;

namespace FlowSimulation;

public interface IBacklogPart
{
  string ToString();
  void Progress();
  bool IsCompleted();
  void ChangeStatusToAssigned();
}

//BUG: that's not a good relaionship. Try to improve it later
//BUG: also, it's not a good name
public interface IBacklogPartCandidate : IBacklogPart 
{
  bool HasName(string itemId);
  bool HasNoPendingDependencies(WorkItemsList workItemsList);
  void AssertDoesNotHaveHigherPriorityThanAnyOfItsDependencies(WorkItemsList workItemsList);
  void AssertDoesNotDependOnItself(WorkItemsList workItemsList);
  void AssertAllDependenciesExist(WorkItemsList workItemsList);
  void AssertRequiresRoleAvailableInThe(Team team);
  void AssertDoesNotDependOn(ImmutableList<string> alreadyEncounteredIds, WorkItemsList workItemsList);
  bool CanBeWorkedOnBy(string role);
  bool IsLessCriticalThan(IBacklogPartCandidate other);
  bool IsMoreCriticalThan(IBacklogPartCandidate other);
  bool HasPriorityHigherThan(ItemPriority priority);
  bool HasPriorityLowerThan(ItemPriority priority);
  void UpdateAssignmentTo(ImmutableList<TeamMember> developers);
}

public class WorkItem(
  string id,
  int points,
  ImmutableList<string> dependencyNames,
  Maybe<string> requiredRole,
  ItemPriority itemPriority) : IBacklogPartCandidate
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

  public void ChangeStatusToAssigned()
  {
    assigned = true;
  }

  public bool HasName(string itemId)
  {
    return id == itemId;
  }

  private ItemPriority Priority => itemPriority;

  public bool HasNoPendingDependencies(WorkItemsList workItemsList)
  {
    var dependencies = workItemsList.FindItemsBy(dependencyNames);
    return workItemsList.AreAllCompleted(dependencies);
  }

  public static IBacklogPartCandidate BasedOn(string itemId, WorkItemProperties workItemProperties)
  {
    return new WorkItem(itemId,
      workItemProperties.Points,
      workItemProperties.Dependencies,
      workItemProperties.RequiredRole, 
      workItemProperties.Priority);
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

  public void AssertDoesNotDependOn(ImmutableList<string> alreadyEncounteredIds, WorkItemsList workItemsList)
  {
    AssertIdIsNoneOf(alreadyEncounteredIds);
    var dependencies = workItemsList.FindItemsBy(dependencyNames);
    foreach (var dependency in dependencies)
    {
      dependency.AssertDoesNotDependOn(alreadyEncounteredIds.Add(id), workItemsList);
    }
  }

  public bool CanBeWorkedOnBy(string role)
  {
    return !IsAssigned() && IsForRole(role);
  }

  private void AssertDoesNotHaveLowerPriorityThan(IBacklogPartCandidate dependency)
  {
    if (dependency.HasPriorityLowerThan(Priority))
    {
      throw new Exception($"{this} has lower priority than its dependencies");
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

  private bool IsForRole(string role)
  {
    return requiredRole.Select(r => r == role).OrTrue();
  }

  public bool IsLessCriticalThan(IBacklogPartCandidate other)
  {
    return other.HasPriorityHigherThan(Priority);
  }

  public bool IsMoreCriticalThan(IBacklogPartCandidate other)
  {
    return other.HasPriorityHigherThan(Priority);
  }

  public bool HasPriorityLowerThan(ItemPriority priority)
  {
    return Priority < priority;
  }

  public void UpdateAssignmentTo(ImmutableList<TeamMember> developers)
  {
    developers.FirstMaybe(d => d.CanWorkOn(this)).Do(d => d.Assign(this));
  }

  public bool HasPriorityHigherThan(ItemPriority priority)
  {
    return Priority > priority;
  }

  private bool IsAssigned()
  {
    return assigned;
  }
}