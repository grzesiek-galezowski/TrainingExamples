using System;
using System.Threading;
using System.Threading.Tasks;
using WarehouseModule.Persistence;

namespace WarehouseModule.AppLogic
{
  public class UpdateOrderStateCommand
  {
    private readonly IOrdersDao _ordersDao;
    private readonly Guid _orderId;
    private readonly OrderStates _newState;

    public UpdateOrderStateCommand(
      Guid orderId,
      OrderStates newState,
      IOrdersDao dbOrdersDao)
    {
      _orderId = orderId;
      _newState = newState;
      _ordersDao = dbOrdersDao;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
      var order = await _ordersDao.FindById(_orderId, cancellationToken);
      await _ordersDao.Save(order with { OrderState = _newState }, cancellationToken);
    }
  }
}