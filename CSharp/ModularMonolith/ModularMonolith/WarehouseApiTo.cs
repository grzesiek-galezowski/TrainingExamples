using System;
using System.Threading;
using System.Threading.Tasks;
using Lib;
using ShopModule;
using WarehouseModule;

namespace ModularMonolith
{
  public class WarehouseApiTo : IWarehouseApi
  {
    private readonly WarehouseModuleInstance _warehouseModule;

    public WarehouseApiTo(WarehouseModuleInstance warehouseModule)
    {
      _warehouseModule = warehouseModule;
    }

    public async Task OrderDelivery(ProductId productId, string deliveryAddress, CancellationToken cancellationToken)
    {
      await _warehouseModule.CommandFactory.CreateOrderDeliveryCommand(productId, deliveryAddress)
        .Execute(cancellationToken);
    }
  }
}