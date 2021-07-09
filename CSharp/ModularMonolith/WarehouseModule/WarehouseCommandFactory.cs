using Lib;

namespace WarehouseModule
{
  public class WarehouseCommandFactory
  {
    private readonly IOrdersDaoFactory _ordersDaoFactory;
    private readonly ICustomerNotifications _customerNotifications;

    public WarehouseCommandFactory(IOrdersDaoFactory ordersDaoFactory, ICustomerNotifications customerNotifications)
    {
      _ordersDaoFactory = ordersDaoFactory;
      _customerNotifications = customerNotifications;
    }

    public OrderDeliveryCommand CreateOrderDeliveryCommand(
      ProductId productId, string deliveryAddress)
    {
      return new OrderDeliveryCommand(
        productId, 
        deliveryAddress, 
        _ordersDaoFactory.CreateOrdersDao(),
        _customerNotifications);
    }
  }
}