namespace WarehouseModule
{
  public interface IOrdersDaoFactory
  {
    IOrdersDao CreateOrdersDao();
  }
}