using ShopModule.AppLogic;
using ShopModule.Persistence;

namespace ShopModule
{
  public class ShopModuleInstance
  {
    public ShopModuleInstance(IWarehouseApi warehouseApi, IProductsDao productsDao)
    {
      CommandFactory = new ShopCommandFactory(warehouseApi, productsDao);
    }

    public static ShopModuleInstance Full(IWarehouseApi warehouseApi)
    {
      return new ShopModuleInstance(warehouseApi, new LiteDbProductsDao());
    }

    public ShopCommandFactory CommandFactory { get; }
  }
}