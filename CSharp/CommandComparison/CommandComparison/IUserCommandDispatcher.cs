namespace CommandComparisonFactory
{
  public interface IUserCommandDispatcher<T>
  {
    void Execute(T command);
  }
}