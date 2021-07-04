using Microsoft.AspNetCore.Http;
using ShopModule;

namespace ModularMonolith
{
  public class GetProductsResponseInProgress : IGetProductsResponseInProgress
  {
    private readonly HttpResponse _response;

    public GetProductsResponseInProgress(HttpResponse response)
    {
      _response = response;
    }
  }
}