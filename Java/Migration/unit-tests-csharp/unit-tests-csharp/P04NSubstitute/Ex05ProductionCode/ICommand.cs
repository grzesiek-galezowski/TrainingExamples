namespace unit_tests_csharp.P04NSubstitute
{
  public interface ICommand
  {
    void ExecuteOn(ISharedCore core);
  }
}