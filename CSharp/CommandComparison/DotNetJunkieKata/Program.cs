namespace DotNetJunkieKata
{
  class Program
  {
    static void Main(string[] args)
    {
      string connectionString = "AAA";
      var handler =
        new DeadlockRetryCommandHandlerDecorator<MoveCustomerCommand>(
          new TransactionCommandHandlerDecorator<MoveCustomerCommand>(
            new MoveCustomerCommandHandler(
              new EntityFrameworkUnitOfWork(connectionString),
              new OtherDependencies()
            )
          )
        );

      // Inject the handler into the controller’s constructor.
      var controller = new CustomerController(handler);

      controller.MoveCustomer(12, new Address());
    }
  }
}
