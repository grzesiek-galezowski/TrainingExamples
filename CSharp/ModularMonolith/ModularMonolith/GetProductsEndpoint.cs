using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopModule;

namespace ModularMonolith
{
  public class GetProductsEndpoint
  {
    private readonly ShopModuleInstance _shopModuleInstance;
    private readonly IShopDaoFactory _shopDaoFactory;

    public GetProductsEndpoint(ShopModuleInstance shopModuleInstance, IShopDaoFactory shopDaoFactory)
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