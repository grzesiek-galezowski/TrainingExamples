namespace FlowSimulation.ProductionCode;

public interface IEventsDestination
{
  void NextDay();
  void ReportItemInProgress(WorkItem workItem, TeamMemberId id, RoleId role);
  void ReportSlack(TeamMemberId s, RoleId role);
  void ReportItemCompleted(ItemId itemId, TeamMemberId memberId, RoleId role);
  void ReportAssignment(TeamMemberId s, WorkItem item, string role);
  void NoItemsOnTheBacklog();
  void NoMembersOnTheTeam();
  void ReportItemGroupCompleted(ItemId id, int pointsFinished);
}