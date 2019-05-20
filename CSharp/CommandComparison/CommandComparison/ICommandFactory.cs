namespace CommandComparisonFactory
{
  public interface ICommandFactory
  {
    AddUserCommand CreateAddUserCommand(UserDto userDto, ResultInProgress resultInProgress);
  }
}