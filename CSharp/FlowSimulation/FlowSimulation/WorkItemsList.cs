using System.Collections.Immutable;

namespace FlowSimulation;

public class WorkItemsList(List<IBacklogPartCandidate> workItems)
{
  public ImmutableList<IBacklogPartCandidate> FindItemsBy(ImmutableList<string> ids)
  {
    return workItems.Where(i => ids.Exists(i.HasName))
      .ToImmutableList();
  }

  public bool Contains(string dependencyName)
  {
    return workItems.Exists(i => i.HasName(dependencyName));
  }

  public bool IsEmpty()
  {
    return workItems.Count == 0;
  }

  public List<IBacklogPartCandidate> AllItems()
  {
    return workItems;
  }

  public ImmutableList<IBacklogPartCandidate> FindNotCompleted()
  {
    return workItems.Where(i => !i.IsCompleted()).ToImmutableList();
  }

  public ImmutableList<IBacklogPartCandidate> FindByItemId(string itemId)
  {
    return workItems.Where(i => i.HasName(itemId)).ToImmutableList();
  }

  public void Add(IBacklogPartCandidate workItem)
  {
    workItems.Add(workItem);
  }

  public bool AreAllCompleted(IEnumerable<IBacklogPartCandidate> dependencies)
  {
    return dependencies.All(item => item.IsCompleted());
  }
}