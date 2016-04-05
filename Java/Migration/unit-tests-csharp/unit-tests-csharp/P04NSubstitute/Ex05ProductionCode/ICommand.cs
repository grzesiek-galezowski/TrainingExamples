namespace unit_tests_csharp.P04NSubstitute.Ex05ProductionCode
{
  public interface ICommand
  {
    void ExecuteOn(ISharedCore core);
  }
}