using System.Threading;
using System.Threading.Tasks;
using Lib;

namespace ShopModule
{
  public interface IWarehouseApi
  {
    Task OrderDelivery(ProductId productId, string deliveryAddress,
      string recipientEmailAddress, CancellationToken cancellationToken);
  }
}