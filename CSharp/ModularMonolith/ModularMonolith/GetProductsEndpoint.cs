using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopModule;

namespace ModularMonolith
{
  public class GetProductsEndpoint
  {
    private readonly ShopModule.ShopModuleInstance _shopModuleInstance;
    private readonly ShopDbContext _shopDbContext;

    public GetProductsEndpoint(ShopModuleInstance shopModuleInstance, ShopDbContext shopDbContext)
    {
      _shopModuleInstance = shopModuleInstance;
      _shopDbContext = shopDbContext;
    }

    public async Task HandleAsync(
      HttpRequest request, 
      HttpResponse response, 
      CancellationToken cancellationToken)
    {
      await _shopModuleInstance.CommandFactory.CreateGetProductsCommand(
          new GetProductsResponseInProgress(response),
          new ProductsDao(_shopDbContext))
        .ExecuteAsync(cancellationToken);
    }
  }
}