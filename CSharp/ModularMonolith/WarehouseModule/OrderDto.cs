using Lib;

namespace WarehouseModule
{
  public record OrderDto(
    ProductId ProductId, 
    string DeliveryAddress, 
    string RecipientEmail,
    OrderStates OrderState);
}