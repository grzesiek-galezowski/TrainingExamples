using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using WarehouseModule.AppLogic;

namespace WarehouseModule.Persistence
{
  public class LiteDbOrdersDao : IOrdersDao
  {
    public Task Save(OrderDto orderDto, CancellationToken cancellationToken)
    {
      using var db = new LiteDatabase(@"C:\Temp\Orders.db");
      var col = db.GetCollection<OrderDto>("orders");
      col.Insert(orderDto);

      return Task.CompletedTask;
    }
  }
}