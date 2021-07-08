using System;
using Microsoft.Extensions.DependencyInjection;
using ShopModule;

namespace ModularMonolith
{
  public class ShopDaoFactory : IShopDaoFactory
  {
    private readonly IServiceProvider _context;

    public ShopDaoFactory(IServiceProvider context)
    {
      _context = context;
    }

    public IProductsDao CreateProductsDao()
    {
      return new ProductsDao(_context.GetRequiredService<ShopDbContext>());
    }
  }
}