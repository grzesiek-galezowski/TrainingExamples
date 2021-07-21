using System.Threading;
using System.Threading.Tasks;

namespace ShopModule.AppLogic
{
  public class BuyProductCommand
  {
    private readonly ProductChoiceDto _choiceDto;
    private readonly IProductsDao _productsDao;
    private readonly IBuyProductResponseInProgress _buyProductResponseInProgress;
    private readonly IShopToWarehouseApi _shopToWarehouseApi;

    public BuyProductCommand(
      ProductChoiceDto choiceDto,
      IProductsDao productsDao,
      IBuyProductResponseInProgress buyProductResponseInProgress,
      IShopToWarehouseApi shopToWarehouseApi)
    {
      _choiceDto = choiceDto;
      _productsDao = productsDao;
      _buyProductResponseInProgress = buyProductResponseInProgress;
      _shopToWarehouseApi = shopToWarehouseApi;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
      var product = await _productsDao.ProductById(_choiceDto.ProductId, cancellationToken);
      product = product with { Quantity = product.Quantity - 1 };
      await _productsDao.Save(product, cancellationToken);
      await _shopToWarehouseApi.OrderDelivery(
        _choiceDto.ProductId, 
        _choiceDto.DeliveryAddress, 
        _choiceDto.RecipientEmailAddress, 
        cancellationToken);
      await _buyProductResponseInProgress.Success(product, cancellationToken);
    }
  }
}