using System;
using Microsoft.Extensions.DependencyInjection;
using ShopModule;
using WarehouseModule;

namespace ModularMonolith
{
  public class OrdersDaoFactory : IOrdersDaoFactory
  {
    private readonly IServiceProvider _context;

    public OrdersDaoFactory(IServiceProvider context)
    {
      _context = context;
    }

    public IOrdersDao CreateOrdersDao()
    {
      return new OrdersDao(_context.GetService<OrdersDbContext>());
    }
  }
}