using FlowSimulation.ProductionCode;

namespace FlowSimulation.Specification.Automation;

public static class WorkItemPropertiesMother
{
  public static WorkItemProperties WithPriority(int priority)
  {
    return new WorkItemProperties(priority: priority);
  }

  public static WorkItemProperties DependingOn(params ItemId[] taskNames)
  {
    return new WorkItemProperties(dependencies: [.. taskNames]);
  }

  public static WorkItemProperties WithPoints(int points)
  {
    return new WorkItemProperties { Points = points };
  }

  public static WorkItemProperties RequiresRole(string developer)
  {
    return new WorkItemProperties(requiredRole: developer);
  }
}