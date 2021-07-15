using System;
using ModularMonolith.BuyProduct;
using ModularMonolith.GetProducts;
using ModularMonolith.InternalCommunication;
using ShopModule;
using WarehouseModule;
using WarehouseModule.AppLogic;

namespace ModularMonolith
{
  public class MonolithApplicationLogicCompositionRoot : IDisposable
  {
    public MonolithApplicationLogicCompositionRoot(
        ICustomerNotifications customerNotifications)
    {
      var warehouseModule = WarehouseModuleInstance.Full(customerNotifications);
      var shopModule = ShopModuleInstance.Full(new WarehouseApiTo(warehouseModule));
      GetProductsEndpoint = new GetProductsEndpoint(shopModule);
      BuyProductEndpoint = new BuyProductEndpoint(shopModule);
    }

    public GetProductsEndpoint GetProductsEndpoint { get; }
    public BuyProductEndpoint BuyProductEndpoint { get; }

    public void Dispose()
    {
    }
  }
}