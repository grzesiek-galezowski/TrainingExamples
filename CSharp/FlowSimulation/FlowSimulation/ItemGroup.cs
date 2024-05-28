using System.Collections.Immutable;

namespace FlowSimulation;

public class ItemGroup(ItemId itemGroupId, ImmutableList<ItemId> children, Events events)
{
  private readonly List<ItemId> completedChildren = [];
  private int pointsFinished = 0;

  public void AddAsParentToItsChildrenIn(WorkItemsList workItemsList)
  {
    foreach (var childId in children)
    {
      var child = workItemsList.FindByItemId(childId).Value();
      child.AddParent(this);
    }
  }

  public void NotifyChildCompleted(ItemId childId, int points)
  {
    completedChildren.Add(childId);
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