using System;

namespace SubscriptionApi.Commands
{
  public class ExceptionLoggedCommand : Command
  {
    private readonly Log _log;
    private readonly Command _wrappedCommand;

    public ExceptionLoggedCommand(Log log, Command wrappedCommand)
    {
      _log = log;
      _wrappedCommand = wrappedCommand;
    }

    public void Invoke()
    {
      try
      {
        _wrappedCommand.Invoke();
      }
      catch (Exception e)
      {
        _log.Error(e);
      }
    }
  }
}