using System.Threading;
using System.Threading.Tasks;
using Lib;

namespace WarehouseModule
{
  public class OrderDeliveryCommand
  {
    private readonly ProductId _productId;
    private readonly string _deliveryAddress;

    public OrderDeliveryCommand(ProductId productId, string deliveryAddress)
    {
      _productId = productId;
      _deliveryAddress = deliveryAddress;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }
  }
}