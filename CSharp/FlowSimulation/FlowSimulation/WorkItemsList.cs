using System.Collections.Immutable;

namespace FlowSimulation;

public class WorkItemsList(List<WorkItem> workItems)
{
  public ImmutableList<WorkItem> FindItemsBy(ImmutableList<string> ids)
  {
    return workItems.Where(i => ids.Exists(i.HasName))
      .ToImmutableList();
  }

  public bool AreAllCompleted(ImmutableList<string> immutableList)
  {
    var dependencies = FindItemsBy(immutableList);
    return AreAllCompleted(dependencies);
  }

  public bool Contains(string dependencyName)
  {
    return workItems.Exists(i => i.HasName(dependencyName));
  }

  public bool IsEmpty()
  {
    return workItems.Count == 0;
  }

  public List<WorkItem> AllItems()
  {
    return workItems;
  }

  public ImmutableList<WorkItem> FindNotCompleted()
  {
    return workItems.Where(i => !i.IsCompleted()).ToImmutableList();
  }

  public ImmutableList<WorkItem> FindByItemId(string itemId)
  {
    return workItems.Where(i => i.HasName(itemId)).ToImmutableList();
  }

  public void Add(WorkItem workItem)
  {
    workItems.Add(workItem);
  }

  public bool AreAllCompleted(IEnumerable<WorkItem> dependencies)
  {
    return dependencies.All(item => item.IsCompleted());
  }
}