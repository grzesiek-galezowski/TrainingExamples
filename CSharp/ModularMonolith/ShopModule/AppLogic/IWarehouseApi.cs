using System.Threading;
using System.Threading.Tasks;

namespace ShopModule.AppLogic
{
  public interface IWarehouseApi
  {
    Task OrderDelivery(ProductId productId, string deliveryAddress,
      string recipientEmailAddress, CancellationToken cancellationToken);
  }
}