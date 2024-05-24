using System.Collections.Immutable;

namespace FlowSimulation;

public class WorkItemsList(List<IVerifiableBacklogPart> workItems)
{
  public ImmutableList<IVerifiableBacklogPart> FindItemsBy(ImmutableList<string> ids)
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

  public List<IVerifiableBacklogPart> AllItems()
  {
    return workItems;
  }

  public ImmutableList<IVerifiableBacklogPart> FindNotCompleted()
  {
    return workItems.Where(i => !i.IsCompleted()).ToImmutableList();
  }

  public ImmutableList<IVerifiableBacklogPart> FindByItemId(string itemId)
  {
    return workItems.Where(i => i.HasName(itemId)).ToImmutableList();
  }

  public void Add(IVerifiableBacklogPart workItem)
  {
    workItems.Add(workItem);
  }

  public bool AreAllCompleted(IEnumerable<IVerifiableBacklogPart> dependencies)
  {
    return dependencies.All(item => item.IsCompleted());
  }
}