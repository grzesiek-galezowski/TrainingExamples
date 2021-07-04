using System.Collections.Generic;

namespace ShopModule
{
  public interface IProductsDao
  {
    IEnumerable<ProductDto> GetAllProducts();
  }
}