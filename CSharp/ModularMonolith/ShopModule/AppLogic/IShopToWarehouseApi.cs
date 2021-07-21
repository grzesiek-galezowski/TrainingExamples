using System.Threading;
using System.Threading.Tasks;

namespace ShopModule.AppLogic
{
  public interface IShopToWarehouseApi
  {
    Task OrderDelivery(ProductId productId, DeliveryAddress deliveryAddress,
      RecipientEmailAddress recipientEmailAddress, CancellationToken cancellationToken);
  }
}