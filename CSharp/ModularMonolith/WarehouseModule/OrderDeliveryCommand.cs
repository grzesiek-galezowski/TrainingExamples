using System.Threading;
using System.Threading.Tasks;
using Lib;

namespace WarehouseModule
{
  public class OrderDeliveryCommand
  {
    private readonly ProductId _productId;
    private readonly string _deliveryAddress;
    private readonly IOrdersDao _ordersDao;
    private readonly ICustomerNotifications _system;

    public OrderDeliveryCommand(
      ProductId productId, 
      string deliveryAddress, 
      IOrdersDao ordersDao, 
      ICustomerNotifications system)
    {
      _productId = productId;
      _deliveryAddress = deliveryAddress;
      _ordersDao = ordersDao;
      _system = system;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
      var orderDto = new OrderDto(_productId, _deliveryAddress, OrderStates.New);
      await _ordersDao.Save(orderDto, cancellationToken);
      _system.NotifyCustomerOfOrderState(orderDto);
    }
  }
}