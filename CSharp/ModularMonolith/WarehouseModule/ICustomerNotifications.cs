namespace WarehouseModule
{
  public interface ICustomerNotifications
  {
    void NotifyCustomerOfOrderState(OrderDto orderDto);
  }
}