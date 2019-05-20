using System.Transactions;

namespace DotNetJunkieKata
{
  public class TransactionCommandHandlerDecorator<TCommand>
    : ICommandHandler<TCommand>
  {
    private readonly ICommandHandler<TCommand> decorated;

    public TransactionCommandHandlerDecorator(
      ICommandHandler<TCommand> decorated)
    {
      this.decorated = decorated;
    }

    public void Handle(TCommand command)
    {
      using (var scope = new TransactionScope())
      {
        this.decorated.Handle(command);

        scope.Complete();
      }
    }
  }
}