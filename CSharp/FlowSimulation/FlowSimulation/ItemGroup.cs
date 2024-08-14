using System.Collections.Immutable;

namespace FlowSimulation;

public class ItemGroup(ItemId itemGroupId, ImmutableList<ItemId> children, IEventsDestination events)
{
  private ImmutableList<ItemId> completedChildren = [];
  private int pointsFinished = 0;

  public void AddAsParentToItsChildrenIn(WorkItemsRepository workItemsRepository)
  {
    foreach (var childId in children)
    {
      var child = workItemsRepository.FindByItemId(childId).Value();
      child.AddParent(this);
    }
  }

  public void NotifyChildCompleted(ItemId childId, int points)
  {
    completedChildren = completedChildren.Add(childId);
    pointsFinished += points;
    if (AllChildrenCompleted())
    {
      events.ReportItemGroupCompleted(itemGroupId, pointsFinished);
    }
  }

  private bool AllChildrenCompleted()
  {
    return children.TrueForAll(completedChildren.Contains);
  }
}