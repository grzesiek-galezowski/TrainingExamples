using System;
using System.Threading;
using System.Threading.Tasks;
using WarehouseModule.Persistence;

namespace WarehouseModule.AppLogic
{
  public class UpdateOrderStateCommand
  {
    private readonly IOrdersDao _ordersDao;
    private readonly IUpdateOrderStateResponseInProgress _responseInProgress;
    private readonly Guid _orderId;
    private readonly OrderStates _newState;

    public UpdateOrderStateCommand(Guid orderId,
      OrderStates newState,
      IOrdersDao dbOrdersDao, 
      IUpdateOrderStateResponseInProgress responseInProgress)
    {
      _orderId = orderId;
      _newState = newState;
      _ordersDao = dbOrdersDao;
      _responseInProgress = responseInProgress;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
      try
      {
        var order = await _ordersDao.FindById(_orderId, cancellationToken);
        var modifiedOrderDto = order with { OrderState = _newState };
        await _ordersDao.Save(modifiedOrderDto, cancellationToken);
        await _responseInProgress.Success(modifiedOrderDto, cancellationToken);
      }
      catch (Exception e)
      {
        await _responseInProgress.Failure(e, cancellationToken);
      }
    }
  }
}