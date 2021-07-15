using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lib;

namespace ShopModule.AppLogic
{
  public interface IProductsDao
  {
    IEnumerable<ProductDto> GetAllProducts();
    Task Save(ProductDto product, CancellationToken cancellationToken);
    ValueTask<ProductDto> ProductById(ProductId productId, CancellationToken cancellationToken);
  }
}