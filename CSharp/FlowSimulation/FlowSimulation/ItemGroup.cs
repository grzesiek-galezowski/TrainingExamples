using System.Collections.Immutable;

namespace FlowSimulation;

public class ItemGroup(ItemId itemGroupId, ImmutableList<ItemId> children, Events events)
{
  private readonly List<ItemId> completedChildren = [];

  public void AddAsParentToItsChildrenIn(WorkItemsList workItemsList)
  {
    foreach (var childId in children)
    {
      var child = workItemsList.FindByItemId(childId).Value();
      child.AddParent(this);
    }
  }

  public void NotifyChildCompleted(ItemId childId)
  {
    completedChildren.Add(childId);
    if (children.TrueForAll(completedChildren.Contains))
    {
      events.ReportItemGroupCompleted(itemGroupId);
    }
  }
}