using FlowSimulation.ProductionCode;
using NSubstitute;

namespace FlowSimulation.Specification;

public static class ExpectedEvents
{
  public static ExpectedEvent Completed(int day, string name, string role, string itemName) =>
    new($"Day {day}: {role} {name} completed the task {itemName}",
      (additionalDestination => additionalDestination.ReportItemCompleted(itemName, name, role)));

  public static ExpectedEvent Slack(int day, string name, string role) =>
    new($"Day {day}: {role} {name} has nothing to work on",
      (additionalDestination => additionalDestination.ReportSlack(name, role)));

  public static ExpectedEvent InProgress(int day, string name, string role, string itemName) =>
    new($"Day {day}: {role} {name} is working on task {itemName}",
      (additionalDestination => additionalDestination.ReportItemInProgress(Arg.Is<WorkItem>(item => item.Has(itemName)), name, role)));

  public static ExpectedEvent Assigned(int day, string name, string role, string itemId) =>
    new($"Day {day}: {role} {name} was assigned to task {itemId}",
      (additionalDestination => additionalDestination.ReportAssignment(name, Arg.Is<WorkItem>(item => item.Has(itemId)), role)));

  public static ExpectedEvent GroupItemDelivered(int day, string groupItemName, int points) =>
    new($"Day {day}: Item group {groupItemName} for {points} points is completed",
      (additionalDestination => additionalDestination.ReportItemGroupCompleted(groupItemName, points)));

  public static ExpectedEvent NoItemsOnTheBacklog() =>
    new("No items on the backlog",
      (additionalDestination => additionalDestination.NoItemsOnTheBacklog()));

  public static ExpectedEvent NoMembersOnTheTeam() =>
    new("No developers on the team",
      (additionalDestination => additionalDestination.NoMembersOnTheTeam()));

  public static ExpectedEvent NextDay() => new("should not be used",
    (additionalDestination => additionalDestination.NextDay()));

}