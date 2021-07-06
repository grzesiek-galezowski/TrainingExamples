using System.Threading;
using System.Threading.Tasks;

namespace ShopModule
{
  public class BuyProductCommand
  {
    private readonly ProductChoiceDto _choiceDto;
    private readonly IProductsDao _productsDao;
    private readonly IBuyProductResponseInProgress _buyProductResponseInProgress;

    public BuyProductCommand(
      ProductChoiceDto choiceDto,
      IProductsDao productsDao,
      IBuyProductResponseInProgress buyProductResponseInProgress)
    {
      _choiceDto = choiceDto;
      _productsDao = productsDao;
      _buyProductResponseInProgress = buyProductResponseInProgress;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      var product = await _productsDao.ProductById(_choiceDto.ProductId, cancellationToken);
      product = product with { Quantity = product.Quantity - 1 };
      await _productsDao.Save(product, cancellationToken);
      await _buyProductResponseInProgress.Success(product, cancellationToken);
    }
  }
}