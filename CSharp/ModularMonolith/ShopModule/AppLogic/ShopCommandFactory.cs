namespace ShopModule.AppLogic
{
  public class ShopCommandFactory
  {
    private readonly IWarehouseApi _warehouseApi;
    private readonly IProductsDao _productsDao;

    public ShopCommandFactory(IWarehouseApi warehouseApi, IProductsDao productsDao)
    {
      _warehouseApi = warehouseApi;
      _productsDao = productsDao;
    }

    public CreateGetProductsCommand CreateGetProductsCommand(IGetProductsResponseInProgress responseInProgress)
    {
      return new CreateGetProductsCommand(responseInProgress, _productsDao);
    }

    public BuyProductCommand CreateBuyProductCommand(
        ProductChoiceDto choiceDto,
      IBuyProductResponseInProgress buyProductResponseInProgress)
    {
      return new BuyProductCommand(choiceDto, _productsDao, buyProductResponseInProgress, _warehouseApi);
    }
  }
}