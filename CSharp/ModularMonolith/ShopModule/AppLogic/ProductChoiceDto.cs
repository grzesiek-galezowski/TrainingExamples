namespace ShopModule.AppLogic
{
  public record ProductChoiceDto(
    ProductId ProductId,
    DeliveryAddress DeliveryAddress,
    RecipientEmailAddress RecipientEmailAddress); //bug not a string?

  public record DeliveryAddress(string Value);
  public record RecipientEmailAddress(string Value);
}