using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ShopModule
{
  public interface IGetProductsResponseInProgress
  {
    Task Success(IEnumerable<ProductDto> allProducts, CancellationToken cancellationToken);
  }
}