using System;
using System.Data.Common;
using System.Threading;

namespace DotNetJunkieKata
{
  public class DeadlockRetryCommandHandlerDecorator<TCommand>
    : ICommandHandler<TCommand>
  {
    private readonly ICommandHandler<TCommand> decoratee;

    public DeadlockRetryCommandHandlerDecorator(
      ICommandHandler<TCommand> decoratee)
    {
      this.decoratee = decoratee;
    }

    public void Handle(TCommand command)
    {
      this.HandleWithRetry(command, retries: 5);
    }

    private void HandleWithRetry(TCommand command, int retries)
    {
      try
      {
        this.decoratee.Handle(command);
      }
      catch (Exception ex)
      {
        if (retries <= 0 || !IsDeadlockException(ex))
          throw;

        Thread.Sleep(300);

        this.HandleWithRetry(command, retries - 1);
      }
    }

    private static bool IsDeadlockException(Exception ex)
    {
      return ex is DbException
             && ex.Message.Contains("deadlock") || ex.InnerException != null && IsDeadlockException(ex.InnerException);
    }
  }
}