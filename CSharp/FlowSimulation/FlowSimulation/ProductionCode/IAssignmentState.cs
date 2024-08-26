namespace FlowSimulation.ProductionCode;

public interface IAssignmentState //bug move
{
  void BeginOn(IAssignmentContext assignment, WorkItem newItem);
  void CloseIfNoWorkLeft(IAssignmentContext assignment, TeamMemberId memberId, RoleId role);
  void Pursue(IAssignmentContext assignment, RoleId role, TeamMemberId memberId);
  bool CanBeWorkedOn();
  void OnEnter();
}