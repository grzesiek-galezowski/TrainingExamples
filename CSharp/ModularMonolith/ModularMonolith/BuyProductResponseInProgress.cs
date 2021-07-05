using Microsoft.AspNetCore.Http;
using ShopModule;

namespace ModularMonolith
{
  public class BuyProductResponseInProgress : IBuyProductResponseInProgress
  {
    public BuyProductResponseInProgress(HttpResponse response)
    {
      
    }
  }
}