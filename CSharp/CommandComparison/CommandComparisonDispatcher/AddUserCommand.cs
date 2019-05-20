using System;

namespace CommandComparisonDispatcher
{
  public class AddUserCommand : IUserCommand
  {
    private readonly User _user;
    private readonly ResultInProgress _resultInProgress;
    private readonly Repository _repository;

    public AddUserCommand(User user, ResultInProgress resultInProgress, Repository repository)
    {
      _user = user;
      _resultInProgress = resultInProgress;
      _repository = repository;
    }

    public void Execute()
    {
      try
      {
        _user.AssertIsValid();
        _user.AddTo(_repository, _resultInProgress);
      }
      catch (Exception e)
      {
        _resultInProgress.FailedForWhateverReason(e);
      }
    }
  }
}