namespace CommandComparisonFactory
{
  public class DispatcherBasedController
  {
    private readonly IResultInProgressFactory _resultInProgressFactory;
    private readonly IUserCommandDispatcher<AddUserCommand> _addUserCommandDispatcher;

    public DispatcherBasedController(
      IResultInProgressFactory resultInProgressFactory, 
      IUserCommandDispatcher<AddUserCommand> addUserCommandDispatcher)
    {
      _resultInProgressFactory = resultInProgressFactory;
      _addUserCommandDispatcher = addUserCommandDispatcher;
    }

    public Result AddUser(UserDto userDto)
    {
      var resultInProgress = _resultInProgressFactory.CreateResultInProgress();
      var command = new AddUserCommand(new User(userDto.Name, userDto.Surname), resultInProgress);
      _addUserCommandDispatcher.Execute(command);
      return resultInProgress.CreateResult();
    }

  }
}