using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopModule;

namespace ModularMonolith
{
  public class BuyProductEndpoint
  {
    private readonly ShopModuleInstance _shopModule;
    private readonly ShopDaoFactory _shopDaoFactory;

    public BuyProductEndpoint(ShopModuleInstance shopModule, ShopDaoFactory shopDaoFactory)
    {
      _shopModule = shopModule;
      _shopDaoFactory = shopDaoFactory;
    }

    public async Task HandleAsync(
      HttpRequest request,
      HttpResponse response,
      CancellationToken cancellationToken)
    {
      var choiceDto = await request.ReadFromJsonAsync<ProductChoiceDto>(cancellationToken);
      await _shopModule.CommandFactory.CreateBuyProductCommand(
        choiceDto,
        _shopDaoFactory.CreateProductsDao(),
        new BuyProductResponseInProgress(response))
        .ExecuteAsync(cancellationToken);

    }
  }
}