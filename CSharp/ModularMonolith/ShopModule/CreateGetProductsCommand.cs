using System.Threading;
using System.Threading.Tasks;

namespace ShopModule
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

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }
  }
}