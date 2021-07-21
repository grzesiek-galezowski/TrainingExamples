
using System;
using WarehouseModule.AppLogic;

namespace WarehouseModule
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
      ProductId productId,
      string deliveryAddress,
      string recipientEmailAddress)
    {
      return new OrderDeliveryCommand(
        productId,
        deliveryAddress,
        recipientEmailAddress,
        _createOrdersDao,
        _customerNotifications);
    }

    public UpdateOrderStateCommand CreateUpdateOrderCommand(Guid orderId, OrderStates newState)
    {
      return new UpdateOrderStateCommand(orderId, newState);
    }
  }
}