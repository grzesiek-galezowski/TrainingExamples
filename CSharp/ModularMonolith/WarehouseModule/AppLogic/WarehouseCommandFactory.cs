using System;

namespace WarehouseModule.AppLogic
{
  public class WarehouseCommandFactory
  {
    private readonly ICustomerNotifications _customerNotifications;
    private readonly IOrdersDao _createOrdersDao;

    public WarehouseCommandFactory(
      ICustomerNotifications customerNotifications,
      IOrdersDao ordersDao)
    {
      _customerNotifications = customerNotifications;
      _createOrdersDao = ordersDao;
    }

    public OrderDeliveryCommand CreateOrderDeliveryCommand(
      ProductId productId,
      DeliveryAddress deliveryAddress,
      RecipientEmailAddress recipientEmailAddress)
    {
      return new OrderDeliveryCommand(
        productId,
        deliveryAddress,
        recipientEmailAddress,
        _createOrdersDao,
        _customerNotifications);
    }

    public UpdateOrderStateCommand CreateUpdateOrderCommand(Guid orderId,
      OrderStates newState, 
      IUpdateOrderStateResponseInProgress responseInProgress)
    {
      return new UpdateOrderStateCommand(orderId, newState, _createOrdersDao, responseInProgress);
    }
  }
}