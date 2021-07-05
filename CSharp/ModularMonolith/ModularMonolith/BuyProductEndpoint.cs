using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopModule;

namespace ModularMonolith
{
  public class BuyProductEndpoint
  {
    private readonly ShopModuleInstance _shopModule;
    private readonly DaoFactory _daoFactory;

    public BuyProductEndpoint(ShopModuleInstance shopModule, DaoFactory daoFactory)
    {
      _shopModule = shopModule;
      _daoFactory = daoFactory;
    }

    public async Task HandleAsync(
      HttpRequest request, 
      HttpResponse response, 
      CancellationToken cancellationToken)
    {
      var choiceDto = await request.ReadFromJsonAsync<ProductChoiceDto>();
      await _shopModule.CommandFactory.CreateBuyProductCommand(
        choiceDto,
        _daoFactory.CreateProductsDao(),
        new BuyProductResponseInProgress(response))
        .ExecuteAsync(cancellationToken);

    }
  }
}