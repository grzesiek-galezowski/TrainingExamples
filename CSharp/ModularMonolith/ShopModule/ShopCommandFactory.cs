namespace ShopModule
{
  public class ShopCommandFactory
  {
    public CreateGetProductsCommand CreateGetProductsCommand(IGetProductsResponseInProgress responseInProgress)
    {
      return new CreateGetProductsCommand();
    }
  }
}