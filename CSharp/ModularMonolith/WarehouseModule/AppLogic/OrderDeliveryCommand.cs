using System.Threading;
using System.Threading.Tasks;

namespace WarehouseModule.AppLogic
{
  public class OrderDeliveryCommand
  {
    private readonly ProductId _productId;
    private readonly string _deliveryAddress;
    private readonly IOrdersDao _ordersDao;
    private readonly ICustomerNotifications _system;
    private readonly string _recipientEmailAddress;

    public OrderDeliveryCommand(
      ProductId productId,
      string deliveryAddress,
      string recipientEmailAddress,
      IOrdersDao ordersDao,
      ICustomerNotifications system)
    {
      _productId = productId;
      _deliveryAddress = deliveryAddress;
      _ordersDao = ordersDao;
      _system = system;
      _recipientEmailAddress = recipientEmailAddress;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
      var orderDto = new OrderDto(
        _productId, 
        _deliveryAddress,
        _recipientEmailAddress,
        OrderStates.New);
      await _ordersDao.Save(orderDto, cancellationToken);
      _system.NotifyCustomerOfOrderState(orderDto);
    }
  }
}