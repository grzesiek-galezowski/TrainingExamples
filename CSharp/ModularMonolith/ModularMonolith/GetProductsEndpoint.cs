using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopModule;

namespace ModularMonolith
{
  public class GetProductsEndpoint
  {
    private readonly ShopModuleInstance _shopModuleInstance;
    private readonly ShopDaoFactory _shopDaoFactory;

    public GetProductsEndpoint(ShopModuleInstance shopModuleInstance, ShopDaoFactory shopDaoFactory)
    {
      _shopModuleInstance = shopModuleInstance;
      _shopDaoFactory = shopDaoFactory;
    }

    public async Task HandleAsync(
      HttpRequest request,
      HttpResponse response,
      CancellationToken cancellationToken)
    {
      await _shopModuleInstance.CommandFactory.CreateGetProductsCommand(
          new GetProductsResponseInProgress(response),
          _shopDaoFactory.CreateProductsDao())
        .ExecuteAsync(cancellationToken);
    }
  }
}