using System;
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
  }
}