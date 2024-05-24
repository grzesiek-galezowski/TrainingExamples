using System.Collections.Immutable;

namespace FlowSimulation;

public class WorkItemsList(List<IBacklogPart> workItems)
{
  public ImmutableList<IBacklogPart> FindItemsBy(ImmutableList<string> ids)
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

  public List<IBacklogPart> AllItems()
  {
    return workItems;
  }

  public ImmutableList<IBacklogPart> FindNotCompleted()
  {
    return workItems.Where(i => !i.IsCompleted()).ToImmutableList();
  }

  public ImmutableList<IBacklogPart> FindByItemId(string itemId)
  {
    return workItems.Where(i => i.HasName(itemId)).ToImmutableList();
  }

  public void Add(IBacklogPart workItem)
  {
    workItems.Add(workItem);
  }

  public bool AreAllCompleted(IEnumerable<IBacklogPart> dependencies)
  {
    return dependencies.All(item => item.IsCompleted());
  }
}