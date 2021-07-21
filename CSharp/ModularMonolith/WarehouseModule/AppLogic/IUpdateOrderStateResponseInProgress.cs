using System;
using System.Threading;
using System.Threading.Tasks;

namespace WarehouseModule.AppLogic
{
  public interface IUpdateOrderStateResponseInProgress
  {
    Task Failure(Exception exception, CancellationToken cancellationToken);
    Task Success(OrderDto orderDto, CancellationToken cancellationToken);
  }
}