using System;
using ShopModule;
using WarehouseModule;

namespace ModularMonolith
{
  public class Monolith : IDisposable
  {
    public Monolith(DaoFactory daoFactory)
    {
      var warehouseModule = new WarehouseModuleInstance();
      var shopModule = new ShopModuleInstance(new WarehouseApiTo(warehouseModule));
      GetProductsEndpoint = new GetProductsEndpoint(shopModule, daoFactory);
      BuyProductEndpoint = new BuyProductEndpoint(shopModule, daoFactory);
    }

    public GetProductsEndpoint GetProductsEndpoint { get; }
    public BuyProductEndpoint BuyProductEndpoint { get; }

    public void Dispose()
    {
    }
  }
}