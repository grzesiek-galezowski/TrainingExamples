using WarehouseModule.AppLogic;
using WarehouseModule.Persistence;

namespace WarehouseModule
{
  public class WarehouseModuleInstance
  {
    public WarehouseModuleInstance(ICustomerNotifications customerNotifications, IOrdersDao ordersDao)
    {
      CommandFactory = new WarehouseCommandFactory(customerNotifications, ordersDao);
    }

    public static WarehouseModuleInstance WithPersistence(ICustomerNotifications customerNotifications)
    {
      return new WarehouseModuleInstance(customerNotifications, new LiteDbOrdersDao());
    }

    public WarehouseCommandFactory CommandFactory { get; }
  }
}