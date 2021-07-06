using System.Threading;
using System.Threading.Tasks;

namespace ShopModule
{
  public interface IBuyProductResponseInProgress
  {
    Task Success(ProductDto product, CancellationToken cancellationToken);
  }
}