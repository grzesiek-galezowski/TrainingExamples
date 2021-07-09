using System;
using System.Threading;
using System.Threading.Tasks;
using WarehouseModule;

namespace ModularMonolith
{
  public class OrdersDao : IOrdersDao
  {
    private readonly OrdersDbContext _ordersDbContext;

    public OrdersDao(OrdersDbContext ordersDbContext)
    {
      _ordersDbContext = ordersDbContext;
    }

    public async Task Save(OrderDto orderDto, CancellationToken cancellationToken)
    {
      _ordersDbContext.Orders.Add(orderDto);
      await _ordersDbContext.SaveChangesAsync(cancellationToken);
    }
  }
}