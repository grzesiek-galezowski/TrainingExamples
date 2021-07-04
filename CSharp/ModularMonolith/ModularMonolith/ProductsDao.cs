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
  }
}