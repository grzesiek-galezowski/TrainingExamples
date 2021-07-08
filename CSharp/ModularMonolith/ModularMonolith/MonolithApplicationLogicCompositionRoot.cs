using System;
using ShopModule;
using WarehouseModule;

namespace ModularMonolith
{
  public class MonolithApplicationLogicCompositionRoot : IDisposable
  {
    public MonolithApplicationLogicCompositionRoot(ShopDaoFactory shopDaoFactory)
    {
      var warehouseModule = new WarehouseModuleInstance();
      var shopModule = new ShopModuleInstance(new WarehouseApiTo(warehouseModule));
      GetProductsEndpoint = new GetProductsEndpoint(shopModule, shopDaoFactory);
      BuyProductEndpoint = new BuyProductEndpoint(shopModule, shopDaoFactory);
    }

    public GetProductsEndpoint GetProductsEndpoint { get; }
    public BuyProductEndpoint BuyProductEndpoint { get; }

    public void Dispose()
    {
    }
  }
}