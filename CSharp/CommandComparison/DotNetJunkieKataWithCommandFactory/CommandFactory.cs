namespace DotNetJunkieKataWithCommandFactory
{
  public class CommandFactory : ICommandFactory
  {
    private readonly EntityFrameworkUnitOfWork _entityFrameworkUnitOfWork;
    private readonly OtherDependencies _otherDependencies;

    public CommandFactory(EntityFrameworkUnitOfWork entityFrameworkUnitOfWork, OtherDependencies otherDependencies)
    {
      _entityFrameworkUnitOfWork = entityFrameworkUnitOfWork;
      _otherDependencies = otherDependencies;
    }

    public ICommand CreateCommand(int customerId, Address newAddress)
    {
      return new DeadlockRetryCommandDecorator(
        new TransactionCommandDecorator(
          new MoveCustomerCommand(
            _entityFrameworkUnitOfWork,
            _otherDependencies, 
            customerId, 
            newAddress)
        )
      );
    }
  }
}