using System.Collections.Immutable;
using Core.Maybe;

namespace FlowSimulation.ProductionCode;

public class WorkItemsRepository(ImmutableList<WorkItem> workItems)
{
  public ImmutableList<WorkItem> FindItemsBy(ImmutableList<ItemId> ids)
  {
    return workItems.Where(i => ids.Exists(i.Has))
      .ToImmutableList();
  }

  public bool Contains(ItemId dependencyName)
  {
    return workItems.Exists(i => i.Has(dependencyName));
  }

  public bool IsEmpty()
  {
    return workItems.Count == 0;
  }

  public ImmutableList<WorkItem> AllItems()
  {
    return workItems.ToImmutableList();
  }

  public ImmutableList<WorkItem> FindNotCompleted()
  {
    return workItems.Where(i => !i.IsCompleted()).ToImmutableList();
  }

  public Maybe<WorkItem> FindByItemId(ItemId itemId)
  {
    return workItems.Find(i => i.Has(itemId)).ToMaybe();
  }

  public void Add(WorkItem workItem)
  {
    workItems = workItems.Add(workItem);
  }
}