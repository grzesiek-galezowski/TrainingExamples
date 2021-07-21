using System.Threading.Tasks;

namespace WarehouseModule
{
  public interface ICustomerNotifications
  {
    void NotifyCustomerOfOrderState(OrderDto orderDto);
  }
}