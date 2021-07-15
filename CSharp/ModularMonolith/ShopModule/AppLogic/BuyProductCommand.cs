using System.Threading;
using System.Threading.Tasks;

namespace ShopModule.AppLogic
{
  public class BuyProductCommand
  {
    private readonly ProductChoiceDto _choiceDto;
    private readonly IProductsDao _productsDao;
    private readonly IBuyProductResponseInProgress _buyProductResponseInProgress;
    private readonly IWarehouseApi _warehouseApi;

    public BuyProductCommand(
      ProductChoiceDto choiceDto,
      IProductsDao productsDao,
      IBuyProductResponseInProgress buyProductResponseInProgress,
      IWarehouseApi warehouseApi)
    {
      _choiceDto = choiceDto;
      _productsDao = productsDao;
      _buyProductResponseInProgress = buyProductResponseInProgress;
      _warehouseApi = warehouseApi;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      var product = await _productsDao.ProductById(_choiceDto.ProductId, cancellationToken);
      product = product with { Quantity = product.Quantity - 1 };
      await _productsDao.Save(product, cancellationToken);
      await _warehouseApi.OrderDelivery(
        _choiceDto.ProductId,
        _choiceDto.DeliveryAddress,
        cancellationToken);
      await _buyProductResponseInProgress.Success(product, cancellationToken);
    }
  }
}