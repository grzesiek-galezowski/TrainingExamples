using System;
using System.Threading;
using System.Threading.Tasks;
using WarehouseModule.Persistence;

namespace WarehouseModule.AppLogic
{
  public class UpdateOrderStateCommand
  {
    private readonly LiteDbOrdersDao _ordersDao;
    private readonly Guid _orderId;
    private readonly OrderStates _newState;

    public UpdateOrderStateCommand(Guid orderId, OrderStates newState)
    {
      _orderId = orderId;
      _newState = newState;
      _ordersDao = new LiteDbOrdersDao();
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
      var order = await _ordersDao.FindById(_orderId, cancellationToken);
      await _ordersDao.Save(order with { OrderState = _newState }, cancellationToken);
    }
  }
}