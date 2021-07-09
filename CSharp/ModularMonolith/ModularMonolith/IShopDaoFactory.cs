using ShopModule;

namespace ModularMonolith
{
  public interface IShopDaoFactory
  {
    IProductsDao CreateProductsDao();
  }
}