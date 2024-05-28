using System.Collections.Immutable;

namespace FlowSimulation;

public class Events
{
  private int day = 1;

  public ImmutableList<string> Entries { get; private set; }
      = ImmutableList<string>.Empty;

  public void MoveToNextDay()
  {
    day++;
  }

  public void ReportItemInProgress(WorkItem workItem, TeamMemberId id, RoleId role)
  {
    AddMessage($"{role} {id} is working on task {workItem}");
  }

  public void ReportSlack(TeamMemberId s, RoleId role)
  {
    AddMessage($"{role} {s} has nothing to work on");
  }

  public void ReportItemCompleted(ItemId itemId, TeamMemberId memberId, RoleId role)
  {
    AddMessage($"{role} {memberId} completed the task {itemId}");
  }

  public void ReportAssignment(TeamMemberId s, WorkItem item, string role)
  {
    AddMessage($"{role} {s} was assigned to task {item}");
  }

  public void NoItemsOnTheBacklog()
  {
    Entries = ["No items on the backlog"];
  }

  public void NoMembersOnTheTeam()
  {
    Entries = ["No developers on the team"];
  }

  private void AddMessage(string message)
  {
    Entries = Entries.Add($"Day {day}: " + message);
  }

  public void ReportItemGroupCompleted(ItemId id, int pointsFinished)
  {
    AddMessage($"Item group {id} for {pointsFinished} points is completed");
  }
}