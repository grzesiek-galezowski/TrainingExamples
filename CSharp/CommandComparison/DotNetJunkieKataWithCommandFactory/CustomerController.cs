namespace DotNetJunkieKataWithCommandFactory
{
  public class CustomerController
  {
    private readonly ICommandFactory _commandFactory;

    public CustomerController(ICommandFactory commandFactory)
    {
      _commandFactory = commandFactory;
    }

    public void MoveCustomer(int customerId, Address newAddress)
    {
      var command = _commandFactory.CreateCommand(customerId, newAddress);
      command.Handle();
    }
  }
}