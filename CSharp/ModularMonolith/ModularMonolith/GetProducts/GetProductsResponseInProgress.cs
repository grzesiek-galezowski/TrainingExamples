using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopModule.AppLogic;

namespace ModularMonolith.GetProducts
{
  public class GetProductsResponseInProgress : IGetProductsResponseInProgress
  {
    private readonly HttpResponse _response;

    public GetProductsResponseInProgress(HttpResponse response)
    {
      _response = response;
    }

    public async Task Success(IEnumerable<ProductDto> allProducts, CancellationToken cancellationToken)
    {
      await _response.WriteAsJsonAsync(allProducts, cancellationToken);
    }
  }
}