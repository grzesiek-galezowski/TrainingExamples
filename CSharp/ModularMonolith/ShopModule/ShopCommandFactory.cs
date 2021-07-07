namespace ShopModule
{
  public class ShopCommandFactory
  {
    private readonly IWarehouseApi _warehouseApi;

    public ShopCommandFactory(IWarehouseApi warehouseApi)
    {
      _warehouseApi = warehouseApi;
    }

    public CreateGetProductsCommand CreateGetProductsCommand(
      IGetProductsResponseInProgress responseInProgress,
      IProductsDao productsDao)
    {
      return new CreateGetProductsCommand(responseInProgress, productsDao);
    }

    public BuyProductCommand CreateBuyProductCommand(ProductChoiceDto choiceDto, IProductsDao productsDao,
      IBuyProductResponseInProgress buyProductResponseInProgress)
    {
      return new BuyProductCommand(choiceDto, productsDao, buyProductResponseInProgress, _warehouseApi);
    }
  }
}