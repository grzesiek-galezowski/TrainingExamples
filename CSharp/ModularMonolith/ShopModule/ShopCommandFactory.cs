namespace ShopModule
{
  public class ShopCommandFactory
  {
    public CreateGetProductsCommand CreateGetProductsCommand(
      IGetProductsResponseInProgress responseInProgress,
      IProductsDao productsDao)
    {
      return new CreateGetProductsCommand(responseInProgress, productsDao);
    }

    public BuyProductCommand CreateBuyProductCommand(ProductChoiceDto choiceDto, IProductsDao productsDao,
      IBuyProductResponseInProgress buyProductResponseInProgress)
    {
      return new BuyProductCommand(choiceDto, productsDao, buyProductResponseInProgress);
    }
  }
}