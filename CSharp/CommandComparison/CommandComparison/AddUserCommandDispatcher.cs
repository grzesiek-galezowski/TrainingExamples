using System;

namespace CommandComparisonFactory
{
  public class AddUserCommandDispatcher : IUserCommandDispatcher<AddUserCommand>
  {
    private readonly Repository _repository;

    public AddUserCommandDispatcher(Repository repository)
    {
      _repository = repository;
    }

    public void Execute(AddUserCommand command)
    {
      try
      {
        command.User.AssertIsValid();
        command.User.AddTo(_repository, command.ResultInProgress);
      }
      catch (Exception e)
      {
        command.ResultInProgress.FailedForWhateverReason(e);
      }
    }
  }
}