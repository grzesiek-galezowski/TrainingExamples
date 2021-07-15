using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopModule;
using ShopModule.AppLogic;

namespace ModularMonolith.BuyProduct
{
  public class BuyProductEndpoint
  {
    private readonly ShopModuleInstance _shopModule;

    public BuyProductEndpoint(ShopModuleInstance shopModule)
    {
      _shopModule = shopModule;
    }

    public async Task Handle(
      HttpRequest request,
      HttpResponse response,
      CancellationToken cancellationToken)
    {
      var choiceDto = await request.ReadFromJsonAsync<ProductChoiceDto>(cancellationToken);
      await _shopModule.CommandFactory.CreateBuyProductCommand(
        choiceDto,
        new BuyProductResponseInProgress(response))
        .Execute(cancellationToken);

    }
  }
}