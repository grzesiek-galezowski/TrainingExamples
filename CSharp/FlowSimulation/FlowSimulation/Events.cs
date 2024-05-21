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

  public void ReportItemInProgress(WorkItem workItem, string id, string role)
  {
    AddMessage($"{role} {id} is working on task {workItem}");
  }

  public void ReportSlack(string s, string role)
  {
    AddMessage($"{role} {s} has nothing to work on");
  }

  public void ReportItemCompleted(WorkItem workItem, string memberId, string role)
  {
    AddMessage($"{role} {memberId} completed the task {workItem}");
  }

  public void ReportAssignment(string s, WorkItem item, string role)
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
}