using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WarehouseModule;
using WarehouseModule.AppLogic;

namespace ModularMonolith.UpdateOrder
{
  public class UpdateOrderEndpoint
  {
    private readonly WarehouseModuleInstance _warehouseModule;

    public UpdateOrderEndpoint(WarehouseModuleInstance warehouseModule)
    {
      _warehouseModule = warehouseModule;
    }

    public async Task Handle(HttpRequest request, HttpResponse response, CancellationToken cancellationToken)
    {
      //bug response in progress
      var first = request.Query["orderId"].First();
      var newGuid = Guid.Parse(first);
      await _warehouseModule.CommandFactory.CreateUpdateOrderCommand(
          newGuid, 
          await request.ReadFromJsonAsync<OrderStates>(cancellationToken))
        .Execute(cancellationToken);
    }
  }
}