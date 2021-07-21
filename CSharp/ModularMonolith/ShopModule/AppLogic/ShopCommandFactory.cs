namespace ShopModule.AppLogic
{
  public class ShopCommandFactory
  {
    private readonly IShopToWarehouseApi _shopToWarehouseApi;
    private readonly IProductsDao _productsDao;

    public ShopCommandFactory(IShopToWarehouseApi shopToWarehouseApi, IProductsDao productsDao)
    {
      _shopToWarehouseApi = shopToWarehouseApi;
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
      return new BuyProductCommand(choiceDto, _productsDao, buyProductResponseInProgress, _shopToWarehouseApi);
    }
  }
}