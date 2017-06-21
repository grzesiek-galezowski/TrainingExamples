namespace AlarmsObjectOriented.Interfaces
{
  public interface TimeCriterion
  {
    bool IsSatisfied();
    void Output();
  }
}