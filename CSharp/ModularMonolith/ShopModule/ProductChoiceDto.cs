namespace ShopModule
{
  public record ProductChoiceDto(ProductId ProductId, string DeliveryAddress); //bug not a string?
  public record ProductId(string Value);
}