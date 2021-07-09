using System.Threading;
using System.Threading.Tasks;

namespace WarehouseModule
{
  public interface IOrdersDao
  {
    Task Save(OrderDto orderDto, CancellationToken cancellationToken);
  }
}