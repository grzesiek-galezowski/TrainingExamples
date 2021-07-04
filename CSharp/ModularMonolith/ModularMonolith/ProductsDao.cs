using System.Collections.Generic;
using System.Linq;
using ShopModule;

namespace ModularMonolith
{
  public class ProductsDao : IProductsDao
  {
    private readonly ShopDbContext _shopDbContext;

    public ProductsDao(ShopDbContext shopDbContext)
    {
      _shopDbContext = shopDbContext;
    }

    public IEnumerable<ProductDto> GetAllProducts()
    {
      return _shopDbContext.Products.AsEnumerable();
    }
  }
}