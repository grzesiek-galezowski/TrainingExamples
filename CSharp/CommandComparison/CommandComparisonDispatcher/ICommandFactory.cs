namespace CommandComparisonDispatcher
{
  public interface ICommandFactory
  {
    IUserCommand CreateAddUserCommand(UserDto userDto, ResultInProgress resultInProgress);
  }
}