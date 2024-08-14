using System.Collections.Immutable;

namespace FlowSimulation;

public class CompoundEvents(ImmutableArray<IEventsDestination> nestedEvents) : IEventsDestination
{
  public void NextDay()
  {
    foreach (var events in nestedEvents)
    {
      events.NextDay();
    }
  }

  public void ReportItemInProgress(WorkItem workItem, TeamMemberId id, RoleId role)
  {
    foreach (var events in nestedEvents)
    {
      events.ReportItemInProgress(workItem, id, role);
    }
  }

  public void ReportSlack(TeamMemberId s, RoleId role)
  {
    foreach (var events in nestedEvents)
    {
      events.ReportSlack(s, role);
    }
  }

  public void ReportItemCompleted(ItemId itemId, TeamMemberId memberId, RoleId role)
  {
    foreach (var events in nestedEvents)
    {
      events.ReportItemCompleted(itemId, memberId, role);
    }
  }

  public void ReportAssignment(TeamMemberId s, WorkItem item, string role)
  {
    foreach (var events in nestedEvents)
    {
      events.ReportAssignment(s, item, role);
    }
  }

  public void NoItemsOnTheBacklog()
  {
    foreach (var events in nestedEvents)
    {
      events.NoItemsOnTheBacklog();
    }
  }

  public void NoMembersOnTheTeam()
  {
    foreach (var events in nestedEvents)
    {
      events.NoMembersOnTheTeam();
    }
  }

  public void ReportItemGroupCompleted(ItemId id, int pointsFinished)
  {
    foreach (var events in nestedEvents)
    {
      events.ReportItemGroupCompleted(id, pointsFinished);
    }
  }
}