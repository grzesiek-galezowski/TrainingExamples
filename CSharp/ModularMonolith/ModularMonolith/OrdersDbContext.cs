using Microsoft.EntityFrameworkCore;
using ShopModule;
using WarehouseModule;

namespace ModularMonolith
{
  public class OrdersDbContext : DbContext
  {
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
      : base(options) { }

    public DbSet<OrderDto> Orders { get; set; }
  } //bug try litedb
}