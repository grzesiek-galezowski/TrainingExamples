using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopModule;

namespace ModularMonolith
{
  public class GetProductsEndpoint
  {
    private readonly ShopModuleInstance _shopModuleInstance;
    private readonly DaoFactory _daoFactory;

    public GetProductsEndpoint(ShopModuleInstance shopModuleInstance, DaoFactory daoFactory)
    {
      _shopModuleInstance = shopModuleInstance;
      _daoFactory = daoFactory;
    }

    public async Task HandleAsync(
      HttpRequest request,
      HttpResponse response,
      CancellationToken cancellationToken)
    {
      await _shopModuleInstance.CommandFactory.CreateGetProductsCommand(
          new GetProductsResponseInProgress(response),
          _daoFactory.CreateProductsDao())
        .ExecuteAsync(cancellationToken);
    }
  }
}