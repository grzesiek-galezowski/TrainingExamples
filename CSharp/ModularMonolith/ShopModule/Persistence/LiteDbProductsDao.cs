using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using ShopModule.AppLogic;

namespace ShopModule.Persistence
{
  public class LiteDbProductsDao : IProductsDao
  {
    public IEnumerable<ProductDto> GetAllProducts()
    {
      using var db = new LiteDatabase(@"C:\Temp\Products.db");
      var col = db.GetCollection<ProductDto>("products");
      var dtos = col.FindAll();
      return dtos;
    }

    public Task Save(ProductDto product, CancellationToken cancellationToken)
    {
      using var db = new LiteDatabase(@"C:\Temp\Products.db");
      var col = db.GetCollection<ProductDto>("products");
      col.Insert(product);

      return Task.CompletedTask;
    }

    public ValueTask<ProductDto> ProductById(ProductId productId, CancellationToken cancellationToken)
    {
      using var db = new LiteDatabase(@"C:\Temp\Products.db");
      var col = db.GetCollection<ProductDto>("products");
      var dto = col.FindById(new BsonValue(productId.Value));
      return ValueTask.FromResult(dto);
    }
  }
}