namespace DotNetJunkieKataWithCommandFactory
{
  public class MoveCustomerCommand : ICommand
  {
    private readonly UnitOfWork db;
    private int _customerId;
    private Address _address;

    public MoveCustomerCommand(
      UnitOfWork db,
      OtherDependencies dep, 
      int customerId, 
      Address address)
    {
      _customerId = customerId;
      _address = address;
      this.db = db;
    }

    public void Handle()
    {
      // TODO: Logic here
    }
  }
}