using System.Threading;
using System.Threading.Tasks;
using ShopModule.AppLogic;
using WarehouseModule;

namespace ModularMonolith.InternalCommunication
{
  public class ShopToWarehouseApiTo : IShopToWarehouseApi
  {
    private readonly WarehouseModuleInstance _warehouseModule;

    public ShopToWarehouseApiTo(WarehouseModuleInstance warehouseModule)
    {
      _warehouseModule = warehouseModule;
    }

    public Task OrderDelivery(ProductId productId,
      DeliveryAddress deliveryAddress,
      RecipientEmailAddress recipientEmailAddress,
      CancellationToken cancellationToken)
    {
      return _warehouseModule.CommandFactory.CreateOrderDeliveryCommand(
              new WarehouseModule.AppLogic.ProductId(productId.Value),
              new WarehouseModule.AppLogic.DeliveryAddress(deliveryAddress.Value),
              new WarehouseModule.AppLogic.RecipientEmailAddress(recipientEmailAddress.Value))
        .Execute(cancellationToken);
    }
  }
}