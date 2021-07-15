using System;
using System.Threading;
using System.Threading.Tasks;

namespace WarehouseModule.AppLogic
{
  public interface IOrdersDao
  {
    Task Save(OrderDto orderDto, CancellationToken cancellationToken);
    Task<OrderDto> FindById(Guid orderId, CancellationToken cancellationToken);
  }
}