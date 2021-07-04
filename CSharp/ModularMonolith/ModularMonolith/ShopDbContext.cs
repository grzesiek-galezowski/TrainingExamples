using Microsoft.EntityFrameworkCore;

namespace ModularMonolith
{
  public class ShopDbContext : DbContext
  {
    public ShopDbContext(DbContextOptions<ShopDbContext> options)
      : base(options) { }

    public DbSet<ProductDto> Products { get; set; }
  }
}