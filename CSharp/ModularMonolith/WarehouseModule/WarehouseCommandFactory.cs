using Lib;

namespace WarehouseModule
{
  public class WarehouseCommandFactory
  {
    public OrderDeliveryCommand CreateOrderDeliveryCommand(ProductId productId, string deliveryAddress)
    {
      return new OrderDeliveryCommand(productId, deliveryAddress);
    }
  }
}