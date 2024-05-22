namespace FlowSimulation;

public interface IAssignmentState //bug move
{
  void BeginOn(IAssignmentContext assignment, WorkItem newItem);
  void CloseIfNoWorkLeft(IAssignmentContext assignment, string memberId, string role);
  void Pursue(IAssignmentContext assignment, string role, string memberId);
}