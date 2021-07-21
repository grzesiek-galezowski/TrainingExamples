namespace WarehouseModule.AppLogic
{
  public record OrderDto(
    ProductId ProductId, 
    DeliveryAddress DeliveryAddress, 
    RecipientEmailAddress RecipientEmail,
    OrderStates OrderState);
}