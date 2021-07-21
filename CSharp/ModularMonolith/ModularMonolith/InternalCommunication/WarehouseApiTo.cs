using System.Threading;
using System.Threading.Tasks;
using ShopModule.AppLogic;
using WarehouseModule;

namespace ModularMonolith.InternalCommunication
{
  public class WarehouseApiTo : IWarehouseApi
  {
    private readonly WarehouseModuleInstance _warehouseModule;

    public WarehouseApiTo(WarehouseModuleInstance warehouseModule)
    {
      _warehouseModule = warehouseModule;
    }

    public Task OrderDelivery(
      ProductId productId,
      string deliveryAddress,
      string recipientEmailAddress,
      CancellationToken cancellationToken)
    {
      return _warehouseModule.CommandFactory.CreateOrderDeliveryCommand(
              new WarehouseModule.AppLogic.ProductId(productId.Value),
              deliveryAddress,
              recipientEmailAddress)
        .Execute(cancellationToken);
    }
  }
}