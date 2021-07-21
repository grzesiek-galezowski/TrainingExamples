using Lib;

namespace ShopModule
{
  public record ProductChoiceDto(ProductId ProductId, string DeliveryAddress, string RecipientEmailAddress); //bug not a string?
}