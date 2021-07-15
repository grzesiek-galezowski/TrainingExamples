using System.Threading;
using System.Threading.Tasks;

namespace ShopModule.AppLogic
{
  public interface IBuyProductResponseInProgress
  {
    Task Success(ProductDto product, CancellationToken cancellationToken);
  }
}