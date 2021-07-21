namespace WarehouseModule.AppLogic
{
  public record OrderDto(
    ProductId ProductId, 
    string DeliveryAddress, 
    string RecipientEmail,
    OrderStates OrderState);
}