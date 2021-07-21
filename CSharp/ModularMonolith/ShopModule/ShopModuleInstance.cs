using ShopModule.AppLogic;
using ShopModule.Persistence;

namespace ShopModule
{
  public class ShopModuleInstance
  {
    public ShopModuleInstance(IShopToWarehouseApi shopToWarehouseApi, IProductsDao productsDao)
    {
      CommandFactory = new ShopCommandFactory(shopToWarehouseApi, productsDao);
    }

    public static ShopModuleInstance Full(IShopToWarehouseApi shopToWarehouseApi)
    {
      return new ShopModuleInstance(shopToWarehouseApi, new LiteDbProductsDao());
    }

    public ShopCommandFactory CommandFactory { get; }
  }
}