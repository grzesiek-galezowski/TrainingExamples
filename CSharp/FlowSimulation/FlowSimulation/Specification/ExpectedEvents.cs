namespace FlowSimulation.Specification;

public static class ExpectedEvents
{
  public static ExpectedEvent Completed(int day, string name, string role, string itemName)
  {
    return new ExpectedEvent($"Day {day}: {role} {name} completed the task {itemName}");
  }

  public static ExpectedEvent Slack(int day, string name, string role)
  {
    return new ExpectedEvent($"Day {day}: {role} {name} has nothing to work on");
  }

  public static ExpectedEvent InProgress(int day, string name, string role, string itemName)
  {
    return new ExpectedEvent($"Day {day}: {role} {name} is working on task {itemName}");
  }

  public static ExpectedEvent Assigned(int day, string name, string role, string itemId)
  {
    return new ExpectedEvent($"Day {day}: {role} {name} was assigned to task {itemId}");
  }

  public static ExpectedEvent GroupItemDelivered(int day, string deliverX, int i)
  {
    return new ExpectedEvent($"Day {day}: Item group {deliverX} for {i} points is completed");
  }

  public static ExpectedEvent NoItemsOnTheBacklog()
  {
    return new ExpectedEvent("No items on the backlog");
  }

  public static ExpectedEvent NoMembersOnTheTeam()
  {
    return new ExpectedEvent("No developers on the team");
  }
}