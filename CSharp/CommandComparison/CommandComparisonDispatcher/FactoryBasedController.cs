namespace CommandComparisonDispatcher
{
  public class FactoryBasedController
  {
    private readonly IResultInProgressFactory _resultInProgressFactory;
    private readonly ICommandFactory _commandFactory;

    public FactoryBasedController(
      IResultInProgressFactory resultInProgressFactory, 
      ICommandFactory commandFactory)
    {
      _resultInProgressFactory = resultInProgressFactory;
      _commandFactory = commandFactory;
    }

    public Result AddUser(UserDto userDto)
    {
      var resultInProgress = _resultInProgressFactory.CreateResultInProgress();
      var command = _commandFactory.CreateAddUserCommand(userDto, resultInProgress);
      command.Execute();
      return resultInProgress.CreateResult();
    }

  }
}