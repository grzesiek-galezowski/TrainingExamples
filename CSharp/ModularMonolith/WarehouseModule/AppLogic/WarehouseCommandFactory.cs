using Lib;

namespace WarehouseModule.AppLogic
{
  public class WarehouseCommandFactory
  {
    private readonly ICustomerNotifications _customerNotifications;
    private IOrdersDao _createOrdersDao;

    public WarehouseCommandFactory(ICustomerNotifications customerNotifications, IOrdersDao ordersDao)
    {
      _customerNotifications = customerNotifications;
      _createOrdersDao = ordersDao;
    }

    public OrderDeliveryCommand CreateOrderDeliveryCommand(
      ProductId productId, string deliveryAddress)
    {
      return new OrderDeliveryCommand(
        productId,
        deliveryAddress,
        _createOrdersDao,
        _customerNotifications);
    }
  }
}