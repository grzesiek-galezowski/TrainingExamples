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
  }
}