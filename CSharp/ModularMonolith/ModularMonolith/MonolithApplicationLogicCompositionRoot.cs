using System;
using ModularMonolith.BuyProduct;
using ModularMonolith.GetProducts;
using ModularMonolith.InternalCommunication;
using ModularMonolith.UpdateOrder;
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
      var shopModule = ShopModuleInstance.Full(new ShopToWarehouseApiTo(warehouseModule));
      GetProductsEndpoint = new GetProductsEndpoint(shopModule);
      BuyProductEndpoint = new BuyProductEndpoint(shopModule);
      UpdateOrderEndpoint = new UpdateOrderEndpoint(warehouseModule);
    }

    public UpdateOrderEndpoint UpdateOrderEndpoint { get; }
    public GetProductsEndpoint GetProductsEndpoint { get; }
    public BuyProductEndpoint BuyProductEndpoint { get; }

    public void Dispose()
    {
    }
  }
}