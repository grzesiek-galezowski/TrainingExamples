namespace WarehouseModule.AppLogic
{
  public record OrderDto(ProductId ProductId, string DeliveryAddress, OrderStates OrderState);
}