using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

    public Task Save(ProductDto product, CancellationToken cancellationToken)
    {
        _shopDbContext.Products.Update(product);
        return _shopDbContext.SaveChangesAsync(cancellationToken);
    }

    public ValueTask<ProductDto> ProductById(ProductId productId, CancellationToken cancellationToken)
    {
        return _shopDbContext.Products.FindAsync(productId);
    }
  }
}