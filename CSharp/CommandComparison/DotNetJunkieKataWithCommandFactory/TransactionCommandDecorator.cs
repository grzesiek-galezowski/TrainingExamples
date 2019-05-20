using System.Transactions;

namespace DotNetJunkieKataWithCommandFactory
{
  public class TransactionCommandDecorator : ICommand
  {
    private readonly ICommand decorated;

    public TransactionCommandDecorator(ICommand decorated)
    {
      this.decorated = decorated;
    }

    public void Handle()
    {
      using (var scope = new TransactionScope())
      {
        this.decorated.Handle();

        scope.Complete();
      }
    }
  }
}