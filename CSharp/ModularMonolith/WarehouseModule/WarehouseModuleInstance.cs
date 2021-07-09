namespace WarehouseModule
{
  public class WarehouseModuleInstance
  {
    public WarehouseModuleInstance(IOrdersDaoFactory ordersDaoFactory, ICustomerNotifications customerNotifications)
    {
      CommandFactory = new WarehouseCommandFactory(ordersDaoFactory, customerNotifications);
    }

    public WarehouseCommandFactory CommandFactory { get; }
  }
}