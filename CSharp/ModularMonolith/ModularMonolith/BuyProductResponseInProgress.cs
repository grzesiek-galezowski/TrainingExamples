using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopModule;

namespace ModularMonolith
{
  public class BuyProductResponseInProgress : IBuyProductResponseInProgress
  {
    private readonly HttpResponse _response;

    public BuyProductResponseInProgress(HttpResponse response)
    {
      _response = response;
    }

    public async Task Success(ProductDto product, CancellationToken cancellationToken)
    {
      await _response.WriteAsJsonAsync(product, cancellationToken);
    }
  }
}