using System.Threading;
using System.Threading.Tasks;

namespace ShopModule.AppLogic
{
  public class CreateGetProductsCommand
  {
    private readonly IGetProductsResponseInProgress _responseInProgress;
    private readonly IProductsDao _productsDao;

    public CreateGetProductsCommand(
      IGetProductsResponseInProgress responseInProgress,
      IProductsDao productsDao)
    {
      _responseInProgress = responseInProgress;
      _productsDao = productsDao;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
      var allProducts = _productsDao.GetAllProducts();
      await _responseInProgress.Success(allProducts, cancellationToken);
    }
  }
}