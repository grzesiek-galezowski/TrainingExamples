using System;
using System.Data.Common;
using System.Threading;

namespace DotNetJunkieKataWithCommandFactory
{
  public class DeadlockRetryCommandDecorator : ICommand
  {
    private readonly ICommand decoratee;

    public DeadlockRetryCommandDecorator(ICommand decoratee)
    {
      this.decoratee = decoratee;
    }

    public void Handle()
    {
      this.HandleWithRetry(retries: 5);
    }

    private void HandleWithRetry(int retries)
    {
      try
      {
        this.decoratee.Handle();
      }
      catch (Exception ex)
      {
        if (retries <= 0 || !IsDeadlockException(ex))
          throw;

        Thread.Sleep(300);

        this.HandleWithRetry(retries - 1);
      }
    }

    private static bool IsDeadlockException(Exception ex)
    {
      return ex is DbException
             && ex.Message.Contains("deadlock") || ex.InnerException != null && IsDeadlockException(ex.InnerException);
    }
  }
}