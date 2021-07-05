using System;
using Microsoft.Extensions.DependencyInjection;
using ShopModule;

namespace ModularMonolith
{
  public class DaoFactory : IDaoFactory
  {
    private readonly IServiceProvider _context;

    public DaoFactory(IServiceProvider context)
    {
      _context = context;
    }

    public IProductsDao CreateProductsDao()
    {
      return new ProductsDao(_context.GetRequiredService<ShopDbContext>());
    }
  }
}