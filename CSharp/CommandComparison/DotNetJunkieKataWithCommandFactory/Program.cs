namespace DotNetJunkieKataWithCommandFactory
{
  class Program
  {
    static void Main(string[] args)
    {
      string connectionString = "AAA";

      // Inject the handler into the controller’s constructor.
      var controller = new CustomerController(
        new CommandFactory(
          new EntityFrameworkUnitOfWork(connectionString), new OtherDependencies()));

      controller.MoveCustomer(12, new Address());
    }
  }
}
