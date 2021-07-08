using System;
using Microsoft.Extensions.DependencyInjection;
using ShopModule;
using WarehouseModule;

namespace ModularMonolith
{
  public interface IOrdersDaoFactory
  {

  }

  public class OrdersDaoFactory : IOrdersDaoFactory
  {
    private readonly IServiceProvider _context;

    public OrdersDaoFactory(IServiceProvider context)
    {
      _context = context;
    }

    public IOrdersDao CreateProductsDao()
    {
      return new OrdersDao(_context.GetRequiredService<OrdersDbContext>());
    }
  }
}