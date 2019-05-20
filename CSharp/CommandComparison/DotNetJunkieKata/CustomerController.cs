namespace DotNetJunkieKata
{
  public class CustomerController
  {
    private ICommandHandler<MoveCustomerCommand> handler;

    public CustomerController(ICommandHandler<MoveCustomerCommand> handler)
    {
      this.handler = handler;
    }

    public void MoveCustomer(int customerId, Address newAddress)
    {
      var command = new MoveCustomerCommand
      {
        CustomerId = customerId,
        NewAddress = newAddress
      };

      this.handler.Handle(command);
    }
  }
}