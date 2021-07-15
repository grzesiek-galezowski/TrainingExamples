namespace WarehouseModule.AppLogic
{
  public interface ICustomerNotifications
  {
    void NotifyCustomerOfOrderState(OrderDto orderDto);
  }
}