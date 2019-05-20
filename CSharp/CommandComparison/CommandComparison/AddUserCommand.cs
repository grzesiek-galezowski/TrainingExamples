namespace CommandComparisonFactory
{
  public class AddUserCommand 
  {
    public readonly User User;
    public readonly ResultInProgress ResultInProgress;

    public AddUserCommand(User user, ResultInProgress resultInProgress)
    {
      User = user;
      ResultInProgress = resultInProgress;
    }
  }
}