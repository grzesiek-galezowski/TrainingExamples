using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopModule;

namespace ModularMonolith.GetProducts
{
  public class GetProductsEndpoint
  {
    private readonly ShopModuleInstance _shopModuleInstance;

    public GetProductsEndpoint(ShopModuleInstance shopModuleInstance)
    {
      _shopModuleInstance = shopModuleInstance;
    }

    public Task Handle(
      HttpRequest request,
      HttpResponse response,
      CancellationToken cancellationToken)
    {
      return _shopModuleInstance.CommandFactory.CreateGetProductsCommand(
          new GetProductsResponseInProgress(response))
        .Execute(cancellationToken);
    }
  }
}