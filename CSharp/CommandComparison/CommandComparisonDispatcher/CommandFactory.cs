namespace CommandComparisonDispatcher
{
  public class CommandFactory : ICommandFactory
  {
    private readonly Repository _repository;

    public CommandFactory(Repository repository)
    {
      _repository = repository;
    }

    public IUserCommand CreateAddUserCommand(UserDto userDto, ResultInProgress resultInProgress)
    {
      return new AddUserCommand(
        new User(userDto.Name, userDto.Surname), 
        resultInProgress, 
        _repository);
    }
  }
}