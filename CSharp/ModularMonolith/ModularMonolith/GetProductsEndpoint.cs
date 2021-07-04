using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ModularMonolith
{
  public class GetProductsEndpoint
  {
    private readonly ShopModule.ShopModule _shopModule;

    public GetProductsEndpoint(ShopModule.ShopModule shopModule)
    {
      _shopModule = shopModule;
    }

    public async Task HandleAsync(
      HttpRequest request, 
      HttpResponse response, 
      CancellationToken cancellationToken)
    {
      await _shopModule.CommandFactory.CreateGetProductsCommand(
          new GetProductsResponseInProgress(response))
        .ExecuteAsync(cancellationToken);
    }
  }
}